using Toolbox.CommandLine;

namespace Docfx.Watcher
{
    class WatcherOptions
    {
        [Option("f"), Position(0)]       
        public string Folder { get; set; }
    }
}
