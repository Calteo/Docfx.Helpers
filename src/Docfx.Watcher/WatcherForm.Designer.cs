
namespace Docfx.Watcher
{
    partial class WatcherForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.layoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.buttonBuild = new System.Windows.Forms.Button();
            this.buttonShow = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.fileSystemWatcher = new System.IO.FileSystemWatcher();
            this.timerWatcher = new System.Windows.Forms.Timer(this.components);
            this.buttonCreateToc = new System.Windows.Forms.Button();
            this.layoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutPanel
            // 
            this.layoutPanel.ColumnCount = 5;
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.Controls.Add(this.buttonOpen, 0, 0);
            this.layoutPanel.Controls.Add(this.textBoxLog, 0, 1);
            this.layoutPanel.Controls.Add(this.buttonBuild, 2, 0);
            this.layoutPanel.Controls.Add(this.buttonShow, 3, 0);
            this.layoutPanel.Controls.Add(this.buttonCreateToc, 1, 0);
            this.layoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanel.Location = new System.Drawing.Point(0, 0);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.RowCount = 2;
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.Size = new System.Drawing.Size(1291, 305);
            this.layoutPanel.TabIndex = 0;
            // 
            // buttonOpen
            // 
            this.buttonOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOpen.Location = new System.Drawing.Point(3, 3);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(154, 28);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.Text = "&Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.ButtonOpenClick);
            // 
            // textBoxLog
            // 
            this.layoutPanel.SetColumnSpan(this.textBoxLog, 5);
            this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLog.Location = new System.Drawing.Point(3, 37);
            this.textBoxLog.MaxLength = 300000;
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLog.Size = new System.Drawing.Size(1285, 265);
            this.textBoxLog.TabIndex = 1;
            this.textBoxLog.WordWrap = false;
            // 
            // buttonBuild
            // 
            this.buttonBuild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonBuild.Enabled = false;
            this.buttonBuild.Location = new System.Drawing.Point(323, 3);
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.Size = new System.Drawing.Size(154, 28);
            this.buttonBuild.TabIndex = 2;
            this.buttonBuild.Text = "Build";
            this.buttonBuild.UseVisualStyleBackColor = true;
            this.buttonBuild.Click += new System.EventHandler(this.ButtonBuildClick);
            // 
            // buttonShow
            // 
            this.buttonShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonShow.Enabled = false;
            this.buttonShow.Location = new System.Drawing.Point(483, 3);
            this.buttonShow.Name = "buttonShow";
            this.buttonShow.Size = new System.Drawing.Size(154, 28);
            this.buttonShow.TabIndex = 3;
            this.buttonShow.Text = "Show";
            this.buttonShow.UseVisualStyleBackColor = true;
            this.buttonShow.Click += new System.EventHandler(this.ButtonShowClick);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Pick docfx folder";
            this.folderBrowserDialog.ShowNewFolderButton = false;
            this.folderBrowserDialog.UseDescriptionForTitle = true;
            // 
            // fileSystemWatcher
            // 
            this.fileSystemWatcher.EnableRaisingEvents = true;
            this.fileSystemWatcher.IncludeSubdirectories = true;
            this.fileSystemWatcher.SynchronizingObject = this;
            this.fileSystemWatcher.Changed += new System.IO.FileSystemEventHandler(this.FileSystemWatcherChanged);
            this.fileSystemWatcher.Created += new System.IO.FileSystemEventHandler(this.FileSystemWatcherCreated);
            this.fileSystemWatcher.Deleted += new System.IO.FileSystemEventHandler(this.FileSystemWatcherDeleted);
            this.fileSystemWatcher.Renamed += new System.IO.RenamedEventHandler(this.FileSystemWatcherRenamed);
            // 
            // timerWatcher
            // 
            this.timerWatcher.Interval = 1000;
            this.timerWatcher.Tick += new System.EventHandler(this.TimerWatcherTick);
            // 
            // buttonCreateToc
            // 
            this.buttonCreateToc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCreateToc.Location = new System.Drawing.Point(163, 3);
            this.buttonCreateToc.Name = "buttonCreateToc";
            this.buttonCreateToc.Size = new System.Drawing.Size(154, 28);
            this.buttonCreateToc.TabIndex = 4;
            this.buttonCreateToc.Text = "&CreateToc";
            this.buttonCreateToc.UseVisualStyleBackColor = true;
            this.buttonCreateToc.Click += new System.EventHandler(this.ButtonCreateTocClick);
            // 
            // WatcherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1291, 305);
            this.Controls.Add(this.layoutPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "WatcherForm";
            this.Text = "DocFx Watcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WatcherFormFormClosing);
            this.Shown += new System.EventHandler(this.WatcherFormShown);
            this.layoutPanel.ResumeLayout(false);
            this.layoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layoutPanel;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.IO.FileSystemWatcher fileSystemWatcher;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Button buttonBuild;
        private System.Windows.Forms.Button buttonShow;
        private System.Windows.Forms.Timer timerWatcher;
        private System.Windows.Forms.Button buttonCreateToc;
    }
}

