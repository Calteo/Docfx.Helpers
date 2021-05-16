using System;
using System.Windows.Forms;
using Toolbox.CommandLine;

namespace Docfx.Watcher
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string [] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var parser = Parser.Create<WatcherOptions>();

            var rc = parser.Parse(args)
                .OnError(r =>
                {
                    MessageBox.Show(r.Text, "DocFx Watcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -2;
                })
                .OnHelp(r =>
                {
                    MessageBox.Show(r.GetHelpText(), "DocFx Watcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 1;
                })
                .On<WatcherOptions>(o =>
                {
                    Application.Run(new WatcherForm { Options = o });
                    return 0;
                })
                .Return;

            return rc;
                        
        }
    }
}
