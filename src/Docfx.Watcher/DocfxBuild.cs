using System.Text.Json.Serialization;

namespace Docfx.Watcher
{
    class DocfxBuild
    {
        [JsonPropertyName("dest")]
        public string Destination { get; set; }

    }
}
