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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browseEmuSteamFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.retroArchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureJoysticksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.retroArchSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageRetroArchFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRetroArchFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(414, 27);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(653, 814);
            this.webBrowser1.TabIndex = 1;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(1, 26);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(410, 785);
            this.treeView1.TabIndex = 2;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.DarkGreen;
            this.button1.Location = new System.Drawing.Point(3, 817);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 24);
            this.button1.TabIndex = 3;
            this.button1.Text = "DOWNLOAD && PLAY";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(152, 817);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(259, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
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
            this.donateToolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.systemToolStripMenuItem.Text = "System";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.aboutToolStripMenuItem.Text = "About EmuSteam";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // browseEmuSteamFolderToolStripMenuItem
            // 
            this.browseEmuSteamFolderToolStripMenuItem.Name = "browseEmuSteamFolderToolStripMenuItem";
            this.browseEmuSteamFolderToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.browseEmuSteamFolderToolStripMenuItem.Text = "Browse EmuSteam Folder";
            this.browseEmuSteamFolderToolStripMenuItem.Click += new System.EventHandler(this.browseEmuSteamFolderToolStripMenuItem_Click);
            // 
            // donateToolStripMenuItem1
            // 
            this.donateToolStripMenuItem1.Name = "donateToolStripMenuItem1";
            this.donateToolStripMenuItem1.Size = new System.Drawing.Size(216, 22);
            this.donateToolStripMenuItem1.Text = "Donate to New Age Soldier";
            this.donateToolStripMenuItem1.Click += new System.EventHandler(this.donateToolStripMenuItem1_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // retroArchToolStripMenuItem
            // 
            this.retroArchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureJoysticksToolStripMenuItem,
            this.retroArchSettingsToolStripMenuItem,
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
            // 
            // retroArchSettingsToolStripMenuItem
            // 
            this.retroArchSettingsToolStripMenuItem.Name = "retroArchSettingsToolStripMenuItem";
            this.retroArchSettingsToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.retroArchSettingsToolStripMenuItem.Text = "RetroArch Settings";
            this.retroArchSettingsToolStripMenuItem.Click += new System.EventHandler(this.retroArchSettingsToolStripMenuItem_Click);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 846);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
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
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem retroArchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureJoysticksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem retroArchSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageRetroArchFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem donateToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openRetroArchFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem browseEmuSteamFolderToolStripMenuItem;
    }
}

