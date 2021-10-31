using System.Linq;
using System;
using System.IO;
using Toolbox.CommandLine;
using Docfx.Core;

namespace Docfx.Create.Toc
{
    class Program
    {
        static int Main(string[] args)
        {
            var parser = Parser.Create<TocOptions>();

            var rc = parser.Parse(args)
                    .OnError(r =>
                    {
                        Console.WriteLine(r.Text);
                        return -1;
                    })
                    .OnHelp(r =>
                    {
                        Console.WriteLine(r.GetHelpText());
                        return 1;
                    })
                    .On<TocOptions>(o => Execute(o)).Return;

            return rc;
        }

        private static int Execute(TocOptions options)
        {
            var folder = Path.GetFullPath(options.Folder ?? Environment.CurrentDirectory);

            return TocFolder.Create(folder, Console.WriteLine);
        }
    }
}
