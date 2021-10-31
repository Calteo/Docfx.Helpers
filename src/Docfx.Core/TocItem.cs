using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Docfx.Core
{
    [DebuggerDisplay("{Name,nq} - {Title}")]
    public class TocItem
    {
        public TocItem(Action<string> report)
        {
            Report = report;
        }

        protected Action<string> Report { get; }

        public string Name { get; set; }
        [YamlHeader("title")]
        public string Title { get; set; }
        [YamlHeader("toc.order")]
        public int Order { get; set; }

        public List<TocItem> Items { get; set; } = new List<TocItem>();


        public Dictionary<string, PropertyInfo> GetHeaders()
        {
            return GetHeaders(GetType());
        }

        public static Dictionary<string, PropertyInfo> GetHeaders(Type type)
        {
            if (!Headers.TryGetValue(type, out var headers))
            {

                Headers[type] = headers = type.GetProperties()
                    .Where(p => p.GetCustomAttribute<YamlHeaderAttribute>() != null)
                    .ToDictionary(p => p.GetCustomAttribute<YamlHeaderAttribute>().Name);
            }
            return headers;
        }

        private static Dictionary<Type, Dictionary<string, PropertyInfo>> Headers { get; } = new Dictionary<Type, Dictionary<string, PropertyInfo>>();


        public virtual void WriteYaml(StreamWriter writer, int indent)
        {
            writer.Write(new string(' ', indent));
            writer.WriteLine($"- name: {Title}");
        }
    }
}
