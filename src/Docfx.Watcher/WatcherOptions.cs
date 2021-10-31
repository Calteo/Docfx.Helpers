using Toolbox.CommandLine;

namespace Docfx.Watcher
{
    class WatcherOptions
    {
        [Option("folder"), Position(0)]       
        public string Folder { get; set; }
    }
}
