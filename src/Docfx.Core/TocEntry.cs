using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Docfx.Core
{
    public class TocEntry : TocItem
    {
        public TocEntry(Action<string> report)
            : base(report)
        {

        }

        public bool IsIndex => Name == "index.md";     

        [YamlHeader("uid")]
        public string Uid { get; private set; }
        
        [YamlHeader("toc.parent")]
        public string Parent { get; private set; }

        [YamlHeader("toc.title")]
        public string TocTitle { get; private set; }

        public TocFolder Linked { get; set; }

        private static Regex KeyValuePattern { get; } = new Regex(@"^\s*(?<key>[^:]+)\s*:\s*(?<value>.+?)\s*$", RegexOptions.Compiled);

        static public TocEntry Scan(FileInfo file, Action<string> report)
        {
            var toc = new TocEntry(report) { Name = file.Name };

            var headers = toc.GetHeaders();

            using (var stream = file.OpenText())
            {
                var line = stream.ReadLine();
                if (line == "---")
                {
                    do
                    {
                        line = stream.ReadLine();
                        var match = KeyValuePattern.Match(line);
                        if (match.Success)
                        {
                            var key = match.Groups["key"].Value;
                            var value = match.Groups["value"].Value;
                            if (headers.TryGetValue(key, out var property))
                            {
                                property.SetValue(toc, Convert.ChangeType(value, property.PropertyType));
                            }
                            else
                            {
                                report($"{file.FullName}: unparsed header: {key}");
                            }
                        }
                    }
                    while (line != "---" && line != "");
                }
            }

            if (toc.Title == null)
            {
                toc.Title = Path.GetFileNameWithoutExtension(file.Name);
                toc.Title = toc.Title.Substring(0, 1).ToUpper() + toc.Title[1..];
            }
            if (toc.Uid == null)
                toc.Uid = Guid.NewGuid().ToString();

            return toc;
        }

        public override void WriteYaml(StreamWriter writer, int indent)
        {
            base.WriteYaml(writer, indent);

            if (Linked == null)
            {
                writer.Write(new string(' ', indent));
                writer.WriteLine($"  href: {Name}");

                if (Items.Count > 0)
                {
                    writer.Write(new string(' ', indent));
                    writer.WriteLine($"  items:");
                }

                foreach (var item in Items)
                {
                    item.WriteYaml(writer, indent + 2);
                }
            }
            else
            {
                writer.Write(new string(' ', indent));
                writer.WriteLine($"  href: {Linked.Name}/toc.yml");
                writer.Write(new string(' ', indent));
                writer.WriteLine($"  topicHref: {Name}");
                Linked.WriteToc();
            }
        }
    }
}
