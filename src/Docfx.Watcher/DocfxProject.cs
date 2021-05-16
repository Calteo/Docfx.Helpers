using System.Text.Json.Serialization;

namespace Docfx.Watcher
{
    class DocfxProject
    {
        [JsonPropertyName("build")]
        public DocfxBuild Build { get; set; }
    }
}
