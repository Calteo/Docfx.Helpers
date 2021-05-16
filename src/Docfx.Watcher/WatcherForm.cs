using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Docfx.Watcher
{
    internal partial class WatcherForm : Form
    {
        public WatcherForm()
        {
            InitializeComponent();

            LogInvoker = new AppendLogInvoker(AppendLog); 
        }

        public string Folder { get; private set; }
        public string Destination { get; private set; }

        private void ButtonOpenClick(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                StopWatching();
                StartWatching(folderBrowserDialog.SelectedPath);
            }
        }

        public WatcherOptions Options { get; set; }

        private const string Title = "DocFx Watcher";

        private void StartWatching(string selectedPath)
        {
            var docfxProjectfile = Path.Combine(selectedPath, "docfx.json");
            if (File.Exists(docfxProjectfile))            
            {
                using (var reader = new StreamReader(docfxProjectfile))
                {
                    var text = reader.ReadToEnd();
                    var project = JsonSerializer.Deserialize<DocfxProject>(text);
                    var site = project.Build.Destination;
                    if (!Path.IsPathRooted(site))
                    {
                        Destination = site;
                    }
                    else
                    {
                        Destination = null;
                    }
                }
                

                Folder = selectedPath;
                Text = $"{Title} - {Folder}";

                fileSystemWatcher.Path = Folder;
                fileSystemWatcher.EnableRaisingEvents = true;
                AppendLog($"start watching '{Folder}'");

                StartDocfx();
            }
            else
            {
                Folder = "";
                Text = $"{Title}";
                AppendLog($"no docfx project in '{Folder}'");
                buttonBuild.Enabled = buttonShow.Enabled = false;
            }
        }

        private void StopWatching()
        {
            Text = Title;
            if (fileSystemWatcher.EnableRaisingEvents)
            {
                fileSystemWatcher.EnableRaisingEvents = false;
                AppendLog($"stopped watching '{Folder}'");
            }
        }

        private delegate void AppendLogInvoker(string text);

        private AppendLogInvoker LogInvoker { get; } 

        private void AppendLog(string text)
        {
            textBoxLog.Text += $"[{DateTime.Now}] {text}" + Environment.NewLine;
            textBoxLog.SelectionLength = 0;
            textBoxLog.SelectionStart = textBoxLog.Text.Length;
            textBoxLog.ScrollToCaret();

            var siteMatch = DocfxSitePattern.Match(text);
            if (siteMatch.Success)
            {
                if (DocfxSite != siteMatch.Groups["site"].Value)
                {
                    DocfxSite = siteMatch.Groups["site"].Value;
                    ShowSite();
                    buttonShow.Enabled = true;
                    buttonBuild.Enabled = true;                    
                }
                Changes = 0;
                timerWatcher.Enabled = true;
            }
        }

        private void ShowSite()
        {
            Process.Start(new ProcessStartInfo { FileName = DocfxSite, UseShellExecute = true, Verb = "open" });
        }


        private Regex DocfxSitePattern = new Regex(@"Serving ""[^""]+"" on (?<site>http:.*)$", RegexOptions.Singleline|RegexOptions.Compiled);
        private string DocfxSite { get; set; }

        public Process Docfx { get; set; }
        public bool Exiting { get; set; }

        private void StopDocfx()
        {
            if (Docfx != null && !Docfx.HasExited)
            {
                Docfx.StandardInput.WriteLine("");               
            }
            Docfx = null;
        }

        private void StartDocfx()
        {
            StopDocfx();

            var startinfo = new ProcessStartInfo
            {
                FileName = "docfx.exe",
                Arguments = "-s",
                WorkingDirectory = Folder,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                CreateNoWindow = true,                
            };
            Docfx = new Process
            {
                StartInfo = startinfo,
                EnableRaisingEvents = true,
            };
            Docfx.OutputDataReceived += DocfxOutputDataReceived;
            Docfx.ErrorDataReceived += DocfxErrorDataReceived;
            Docfx.Exited += DocfxExited;
            

            var rc = Docfx.Start();
            if (rc)
            {                
                AppendLog($"docfx[{Docfx.Id}] started");
                Docfx.BeginOutputReadLine();
                Docfx.BeginErrorReadLine();
            }
            else
            {
                AppendLog("docfx not started");
                Docfx = null;
            }
        }

        private void DocfxExited(object sender, EventArgs e)
        {
            /*
            var process = (Process)sender;
            Invoke(LogInvoker, $"docfx exited - {process.ExitCode}");
            */
        }

        private void DocfxErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            var process = (Process)sender;

            if (e.Data != null)
                Invoke(LogInvoker, $"docfx[{process.Id}] ERROR: {e.Data}");
        }

        private void DocfxOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            var process = (Process)sender;

            if (e.Data != null)
            {                
                Invoke(LogInvoker, $"docfx[{process.Id}]: {e.Data}");
            }
        }

        private void WatcherFormShown(object sender, EventArgs e)
        {
            if (Options.Folder != null)
            {
                StartWatching(Options.Folder);
            }
        }

        private void ButtonBuildClick(object sender, EventArgs e)
        {
            StartDocfx();
        }

        private void WatcherFormFormClosing(object sender, FormClosingEventArgs e)
        {
            Exiting = true;
            StopDocfx();
        }

        private void ButtonShowClick(object sender, EventArgs e)
        {
            ShowSite();
        }

        private void FileSystemWatcherChanged(object sender, FileSystemEventArgs e)
        {
            RegisterChange(e.Name);
        }

        private void FileSystemWatcherCreated(object sender, FileSystemEventArgs e)
        {
            RegisterChange(e.Name);
        }

        private void FileSystemWatcherDeleted(object sender, FileSystemEventArgs e)
        {
            RegisterChange(e.Name);
        }

        private void FileSystemWatcherRenamed(object sender, RenamedEventArgs e)
        {
            RegisterChange(e.Name);
        }

        private void RegisterChange(string name)
        {
            if (name.StartsWith(@"obj\")) return;
            if (Destination != null && name.StartsWith(Destination)) return;

            Changes++;
        }

        public int Changes { get; set; }

        private void TimerWatcherTick(object sender, EventArgs e)
        {
            if (Changes > 0)
            {
                timerWatcher.Enabled = false;
                StartDocfx();
            }
        }
    }
}