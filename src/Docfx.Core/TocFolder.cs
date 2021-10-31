using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Docfx.Core
{
    public class TocFolder : TocItem
    {
        public static int Create(string folder, Action<string> report)
        {
            report($"scan folder '{folder}'");

            if (!File.Exists(Path.Combine(folder, "docfx.json")))
            {
                report($"no docfx project found.");
                return -2;
            }

            var rootToc = Scan(new DirectoryInfo(folder), report);
            var index = rootToc.Items.OfType<TocEntry>().FirstOrDefault();
            if (index?.IsIndex ?? false)
            {
                rootToc.Items.Remove(index);
            }

            rootToc.WriteToc();

            report($"scan completed '{folder}'");

            return 0;
        }

        private static TocFolder Scan(DirectoryInfo directory, Action<string> report)
        {
            var files = directory.GetFiles("*.md").ToList();
            if (files.Count == 0) return null;

            var toc = new TocFolder(report)
            { 
                Folder = directory, 
                Name = directory.Name 
            };
            var entries = files.Select(f => TocEntry.Scan(f, report)).ToList();
            var index = entries.FirstOrDefault(e => e.IsIndex);

            var children = entries.Where(e => e.Parent != null);
            entries = entries.Except(children).ToList();

            var parents = entries.ToDictionary(e => e.Uid);
            var childsByParent = children.GroupBy(c => c.Parent).ToDictionary(g => g.Key, g => g.OrderBy(c => c.Order).ThenBy(c => c.Title));

            while (childsByParent.Count > 0)
            {
                var start = childsByParent.Count;
                foreach (var parent in parents.Values.ToArray())
                {
                    if (childsByParent.TryGetValue(parent.Uid, out var theChildren))
                    {
                        parent.Items = theChildren.Cast<TocItem>().ToList();
                        childsByParent.Remove(parent.Uid);
                        foreach (var child in theChildren)
                        {
                            parents.Add(child.Uid, child);
                        }
                    }
                }
                if (start == childsByParent.Count)
                {
                    report($"parents not found '{directory.FullName}'");
                    foreach (var childGroup in childsByParent)
                    {
                        report($"{childGroup.Key}:");
                        foreach (var child in childGroup.Value)
                        {
                            report($"- {child.Name}:");
                        }
                    }
                }
            }

            var directoryItems = directory.GetDirectories().Select(d => Scan(d, report)).Where(d => d != null).ToList();
            var entryNames = entries.ToDictionary(e => e.Name);

            foreach (var directoryItem in directoryItems.ToArray())
            {
                if (entryNames.TryGetValue(directoryItem.Name + ".md", out var entry))
                {
                    if (entry.Items.Count == 0)
                    {
                        report($"manual child will be ignored by folder content '{entry.Name}'");
                    }
                    entry.Linked = directoryItem;
                    directoryItems.Remove(directoryItem);
                }
            }

            if (index != null)
            {
                entries.Remove(index);
            }

            toc.Items = entries.Cast<TocItem>().Concat(directoryItems).ToList();

            toc.Items = toc.Items.OrderBy(e => e.Order).ThenBy(e => e.Title).ToList();
            if (index != null)
            {
                toc.Items.Insert(0, index);
                toc.Title = index.TocTitle ?? index.Title;
                toc.Order = index.Order;                
            }

            return toc;
        }

        public TocFolder(Action<string> report)
            : base(report)
        {
        }

        public DirectoryInfo Folder { get; private set; }

        public void WriteToc()
        {
            var filename = Path.Combine(Folder.FullName, "toc.yml");
            Report($"write '{filename}'");

            using (var writer = new StreamWriter(filename, false, new UTF8Encoding(false)))
            {
                foreach (var item in Items)
                {
                    item.WriteYaml(writer, 0);
                }
            }
        }

        public override void WriteYaml(StreamWriter writer, int indent)
        {
            base.WriteYaml(writer, indent);

            writer.Write(new string(' ', indent));
            writer.WriteLine($"  href: {Name}/");

            WriteToc();
        }
    }
}
