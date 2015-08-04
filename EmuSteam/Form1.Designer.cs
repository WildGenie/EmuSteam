namespace EmuSteam
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browseEmuSteamFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.retroArchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureJoysticksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageRetroArchFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRetroArchFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dlprogressLabel = new System.Windows.Forms.Label();
            this.loadingLabel = new System.Windows.Forms.Label();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(288, 27);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(779, 726);
            this.webBrowser1.TabIndex = 1;
            this.webBrowser1.Url = new System.Uri("http://newagesoldier.com/myfiles/emusteam/storefront", System.UriKind.Absolute);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(1, 27);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(281, 695);
            this.treeView1.TabIndex = 2;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.DarkGreen;
            this.button1.Location = new System.Drawing.Point(3, 728);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 24);
            this.button1.TabIndex = 3;
            this.button1.Text = "DOWNLOAD && PLAY";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(152, 729);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(130, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.WorkerSupportsCancellation = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem,
            this.retroArchToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1069, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.browseEmuSteamFolderToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.systemToolStripMenuItem.Text = "System";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.aboutToolStripMenuItem.Text = "About EmuSteam";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // browseEmuSteamFolderToolStripMenuItem
            // 
            this.browseEmuSteamFolderToolStripMenuItem.Name = "browseEmuSteamFolderToolStripMenuItem";
            this.browseEmuSteamFolderToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.browseEmuSteamFolderToolStripMenuItem.Text = "Browse EmuSteam Folder";
            this.browseEmuSteamFolderToolStripMenuItem.Click += new System.EventHandler(this.browseEmuSteamFolderToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // retroArchToolStripMenuItem
            // 
            this.retroArchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureJoysticksToolStripMenuItem,
            this.manageRetroArchFolderToolStripMenuItem,
            this.openRetroArchFolderToolStripMenuItem});
            this.retroArchToolStripMenuItem.Name = "retroArchToolStripMenuItem";
            this.retroArchToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.retroArchToolStripMenuItem.Text = "RetroArch";
            // 
            // configureJoysticksToolStripMenuItem
            // 
            this.configureJoysticksToolStripMenuItem.Name = "configureJoysticksToolStripMenuItem";
            this.configureJoysticksToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.configureJoysticksToolStripMenuItem.Text = "Configure Joysticks";
            this.configureJoysticksToolStripMenuItem.Click += new System.EventHandler(this.configureJoysticksToolStripMenuItem_Click);
            // 
            // manageRetroArchFolderToolStripMenuItem
            // 
            this.manageRetroArchFolderToolStripMenuItem.Name = "manageRetroArchFolderToolStripMenuItem";
            this.manageRetroArchFolderToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.manageRetroArchFolderToolStripMenuItem.Text = "Get/Delete RetroArch";
            this.manageRetroArchFolderToolStripMenuItem.Click += new System.EventHandler(this.manageRetroArchFolderToolStripMenuItem_Click);
            // 
            // openRetroArchFolderToolStripMenuItem
            // 
            this.openRetroArchFolderToolStripMenuItem.Name = "openRetroArchFolderToolStripMenuItem";
            this.openRetroArchFolderToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.openRetroArchFolderToolStripMenuItem.Text = "Browse RetroArch Folder";
            this.openRetroArchFolderToolStripMenuItem.Click += new System.EventHandler(this.openRetroArchFolderToolStripMenuItem_Click);
            // 
            // dlprogressLabel
            // 
            this.dlprogressLabel.AutoSize = true;
            this.dlprogressLabel.BackColor = System.Drawing.Color.Transparent;
            this.dlprogressLabel.Location = new System.Drawing.Point(162, 734);
            this.dlprogressLabel.Name = "dlprogressLabel";
            this.dlprogressLabel.Size = new System.Drawing.Size(111, 13);
            this.dlprogressLabel.TabIndex = 6;
            this.dlprogressLabel.Text = "Download is starting...";
            this.dlprogressLabel.Visible = false;
            // 
            // loadingLabel
            // 
            this.loadingLabel.AutoSize = true;
            this.loadingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 85F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadingLabel.Location = new System.Drawing.Point(371, 259);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(556, 128);
            this.loadingLabel.TabIndex = 7;
            this.loadingLabel.Text = "Loading...";
            this.loadingLabel.Visible = false;
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 756);
            this.Controls.Add(this.loadingLabel);
            this.Controls.Add(this.dlprogressLabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "EmuSteam";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem retroArchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureJoysticksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageRetroArchFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openRetroArchFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem browseEmuSteamFolderToolStripMenuItem;
        private System.Windows.Forms.Label dlprogressLabel;
        private System.Windows.Forms.Label loadingLabel;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
    }
}

