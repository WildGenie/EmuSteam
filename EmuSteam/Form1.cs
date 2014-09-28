using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Web;
using System.IO;
using System.Net;
using Ionic.Zip;
using System.Diagnostics;
using Microsoft.DirectX.DirectInput;

namespace EmuSteam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string RetroarchDir = Application.StartupPath + @"\retroarch";

        private void Form1_Load(object sender, EventArgs e)
        {
            string masterRomDir = Application.StartupPath + @"\roms";
            if (!Directory.Exists(RetroarchDir))
            {
                Form2 form2 = new Form2();
                form2.Show();
            }
            if (!Directory.Exists(masterRomDir))
                Directory.CreateDirectory(masterRomDir);
            string retroarchDir = Application.StartupPath + @"\configs";
            if (!Directory.Exists(retroarchDir))
                Directory.CreateDirectory(retroarchDir);

            backgroundWorkerSettings ("http://newagesoldier.com/myfiles/xml/emusteam/xml.php", "console", "fullname", "link", "", "" );
        }

        private void backgroundWorkerSettings(string url, string parentNode, string nodeName, string nodeURL, string parentListView, string childListView)
        {
            string[] argArray = { url, parentNode, nodeName, nodeURL, parentListView, childListView };
            this.UseWaitCursor = true;
            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync(argArray);
        }

        public void getRetroArch()
        {
            if (!Directory.Exists(RetroarchDir))
                Directory.CreateDirectory(RetroarchDir);

        }

        private string ConvertStringArrayToString(string[] array)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append('.');
            }
            return builder.ToString();
        }

        private void Item_Click(object sender, EventArgs e)
        { //needs to be modified for USB joystick controls
            string Console = ((ToolStripMenuItem)sender).OwnerItem.OwnerItem.Text;
            string Controller = ((ToolStripMenuItem)sender).OwnerItem.Text;
            string joystickNum = ((ToolStripMenuItem)sender).Name;
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;

            string joystickFile = Application.StartupPath + @"\configs\" + Console + " " + Controller + ".cfg";
            if (!File.Exists(joystickFile))
                File.Create(joystickFile).Close();

            string pNum = "";
            if (Controller.Contains("Controller 1"))
                pNum = " -p 1";
            else
                pNum = " -p 2";

            string strCmdText = Application.StartupPath + @"\retroarch\retroarch-joyconfig.exe";
            string arguments = @"-o " + '"' + joystickFile + '"' + pNum + " -j " + joystickNum;
            Process.Start(strCmdText, arguments);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] args = (string[])e.Argument;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(args[0]);

            if (treeView1.InvokeRequired)
                treeView1.Invoke(new MethodInvoker(delegate
                {
                    XmlNodeList userNodes = xmlDoc.SelectNodes("//" + args[1]);
                    foreach (XmlNode userNode in userNodes)
                    {
                        TreeNode mainNode = new TreeNode();
                        string realURLP = HttpUtility.HtmlDecode(args[4]);
                        string realURLC = HttpUtility.HtmlDecode(args[5]);
                        mainNode.Text = userNode.SelectSingleNode(args[2]).InnerXml;
                        mainNode.Name = userNode.SelectSingleNode(args[3]).InnerXml;
                        if (string.IsNullOrEmpty(args[4]) && string.IsNullOrEmpty(args[5]))
                        { //first time loading console list
                            treeView1.Nodes.Add(mainNode);

                            //add joystick menus
                            ToolStripMenuItem item = new ToolStripMenuItem("CMainMenu");
                            item.Text = mainNode.Text;
                            configureJoysticksToolStripMenuItem.DropDown.Items.Add(item);

                            ToolStripMenuItem Pitem1 = new ToolStripMenuItem("CMenu1");
                            Pitem1.Text = "Controller 1";
                            item.DropDown.Items.Add(Pitem1);

                            DeviceList gameControllerList = Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly);
                            int i = 0;
                            foreach (DeviceInstance di in gameControllerList)
                            {
                                ToolStripMenuItem USBitem = new ToolStripMenuItem("USBitem");
                                USBitem.Text = di.InstanceName;
                                USBitem.Name = i.ToString();
                                USBitem.Click += new EventHandler(Item_Click);
                                Pitem1.DropDown.Items.Add(USBitem);
                                i++;
                            }

                            ToolStripMenuItem Pitem2 = new ToolStripMenuItem("CMenu2");
                            Pitem2.Text = "Controller 2";
                            item.DropDown.Items.Add(Pitem2);

                            DeviceList gameControllerList2 = Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly);
                            foreach (DeviceInstance di in gameControllerList2)
                            {
                                ToolStripMenuItem USBitem2 = new ToolStripMenuItem("USBitem2");
                                USBitem2.Text = di.InstanceName;
                                USBitem2.Click += new EventHandler(Item_Click);
                                Pitem2.DropDown.Items.Add(USBitem2);
                            }
                        }
                        else if (!string.IsNullOrEmpty(args[4]) && string.IsNullOrEmpty(args[5])) //we selected a console
                        {
                            treeView1.Nodes[Convert.ToInt32(realURLP)].Nodes.Add(mainNode);
                            treeView1.Nodes[Convert.ToInt32(realURLP)].Expand();
                        }
                        else //we selected a letter
                        {
                            treeView1.Nodes[Convert.ToInt32(realURLP)].Nodes[Convert.ToInt32(realURLC)].Nodes.Add(mainNode);
                            treeView1.Nodes[Convert.ToInt32(realURLP)].Nodes[Convert.ToInt32(realURLC)].Expand();
                        }
                    }
               }));
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                string realURL = HttpUtility.HtmlDecode(e.Node.Name);
                button1.Enabled = false;
                button1.Text = "Loading...";
                if (realURL.Contains("download.php")) //selected a rom
                {
                    string gameName = e.Node.Text.Replace(" ", "+");
                    string platName = e.Node.Parent.Parent.Text.Replace(" ", "+");
                    int index = gameName.IndexOf(" ("); //remove languages in brackets
                    if (index > 0)
                        gameName = gameName.Substring(0, index);
                    this.UseWaitCursor = true;
                    string navURL = "http://newagesoldier.com/myfiles/xml/thegamesdb/xml.php?n=" + gameName + "&p=" + platName;
                    webBrowser1.Navigate(new Uri(navURL));
                    //MessageBox.Show(navURL); //DEBUG
                }
                else if (!realURL.Contains("l=")) //selected console
                {
                    if (e.Node.Nodes.Count == 0)
                        backgroundWorkerSettings(realURL, "alpha", "letter", "link", e.Node.Index.ToString(), "");
                }
                else if (realURL.Contains("l=")) //selected letter
                {
                    if (e.Node.Nodes.Count == 0)
                        backgroundWorkerSettings(realURL, "rom", "title", "link", e.Node.Parent.Index.ToString(), e.Node.Index.ToString());
                }
            }
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            progressBar1.Text = e.ProgressPercentage.ToString() + "%";
        }

        public void ExtractFileToDirectory(string zipFileName, string outputDirectory)
        {
            FileStream fs = File.OpenRead(zipFileName);
            ZipFile zip = ZipFile.Read(fs);
            Directory.CreateDirectory(outputDirectory);
            foreach (ZipEntry e in zip)
            {
                if (!e.FileName.Contains(".txt") && !e.FileName.Contains(".htm"))
                    e.Extract(outputDirectory, ExtractExistingFileAction.OverwriteSilently);
            }
            fs.Close();

            string tmpFile = outputDirectory + @"\tmp.zip";
            File.Delete(tmpFile);
        }

        private string coreDetect(string console)
        {
            //all work on libretro (http://www.libretro.com/index.php/ecosystem/)
            if (console.Contains("Arcade"))
                return "mame_libretro";
            else if (console.Contains("Nintendo Entertainment System (NES)"))
                return "nestopia_libretro";
            else if (console.Contains("Nintendo Game Boy"))
                return "gambatte_libretro";
            else if (console.Contains("Nintendo Game Boy Advance"))
                return "mednafen_gba_libretro";
            else if (console.Contains("Nintendo 64"))
                return "mupen64plus_libretro";
            else if (console.Contains("Nintendo DS"))
                return "desmume_libretro";
            else if (console.Contains("NeoGeo Pocket"))
                return "mednafen_ngp_libretro";
            else if (console.Contains("Atari 2600"))
                return "stella_libretro";
            else if (console.Contains("Atari Lynx"))
                return "handy_libretro";
            else if (console.Contains("Sony Playstation"))
                return "mednafen_psx_libretro";
            else if (console.Contains("Super Nintendo (SNES)"))
                return "snes9x_libretro";
            else if (console.Contains("Sega CD") || console.Contains("Sega Game Gear") || console.Contains("Sega Genesis") || console.Contains("Sega Master System"))
                return "genesis_plus_gx_libretro";
            else
                return "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] argArray = { treeView1.SelectedNode.Parent.Parent.Text, treeView1.SelectedNode.Name };
            
            string romDir = Application.StartupPath + @"\roms\" + treeView1.SelectedNode.Parent.Parent.Text;
            if (!Directory.Exists(romDir))
                Directory.CreateDirectory(romDir);

            string[] dirs = Directory.GetFiles(romDir, treeView1.SelectedNode.Text + @".*");

            if (dirs == null || dirs.Length == 0) //begin the download
            {
                button1.Enabled = false;
                if (!backgroundWorker2.IsBusy)
                    backgroundWorker2.RunWorkerAsync(argArray);
            }
            else
            {
                //MessageBox.Show(dirs[0]);
                button1.Text = "PLAY GAME!";

                string strCmdText = Application.StartupPath + @"\retroarch\retroarch.exe";
                string fs = "";
                if (Properties.Settings.Default.fullscreen == true)
                    fs = " -f ";
                mergeFiles(treeView1.SelectedNode.Parent.Parent.Text);
                string confFile = "--appendconfig " + '"' + Application.StartupPath + @"\configs\" + treeView1.SelectedNode.Parent.Parent.Text + " merge.cfg" + '"' + " ";

                string arguments = @"-L " + Application.StartupPath + @"\retroarch\cores\" + coreDetect(treeView1.SelectedNode.Parent.Parent.Text) + ".dll " + fs + confFile + " " + '"' + dirs[0] + '"';
                Process.Start(strCmdText, arguments);
            }
        }

        private void mergeFiles(string consoleName)
        {
            string prefix = Application.StartupPath + @"\configs\";
            if (!File.Exists(prefix + consoleName + " Controller 1.cfg"))
                File.Create(prefix + consoleName + " Controller 1.cfg").Close();
            if (!File.Exists(prefix + consoleName + " Controller 2.cfg"))
                File.Create(prefix + consoleName + " Controller 2.cfg").Close();

            const int chunkSize = 2 * 1024; // 2KB
            var inputFiles = new[] { consoleName + " Controller 1.cfg", consoleName + " Controller 2.cfg", "defaults.cfg" };
            using (var output = File.Create(prefix + consoleName + " merge.cfg"))
            {
                foreach (var file in inputFiles)
                {
                    using (var input = File.OpenRead(prefix + file))
                    {
                        var buffer = new byte[chunkSize];
                        int bytesRead;
                        while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            output.Write(buffer, 0, bytesRead);
                        }
                    }
                }
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e) //our background downloader.
        {
            string[] args = (string[])e.Argument;
            string romDir = Application.StartupPath + @"\roms\" + args[0];
            string realURL = HttpUtility.HtmlDecode(args[1]);

            string sUrlToReadFileFrom = realURL;
            string sFilePathToWriteFileTo = romDir + @"\tmp.zip";

            Uri url = new Uri(sUrlToReadFileFrom);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Close();
            long iSize = response.ContentLength;
            Int64 iRunningByteTotal = 0;

            using (WebClient client = new WebClient())
            {
                using (Stream streamRemote = client.OpenRead(new Uri(sUrlToReadFileFrom)))
                {
                    using (Stream streamLocal = new FileStream(sFilePathToWriteFileTo, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        int iByteSize = 0;
                        byte[] byteBuffer = new byte[iSize];
                        while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                        {
                            streamLocal.Write(byteBuffer, 0, iByteSize);
                            iRunningByteTotal += iByteSize;

                            double dIndex = (double)(iRunningByteTotal);
                            double dTotal = (double)byteBuffer.Length;
                            double dProgressPercentage = (dIndex / dTotal);
                            int iProgressPercentage = (int)(dProgressPercentage * 100);
                            if (dIndex > 0 && dTotal > 0)
                                backgroundWorker2.ReportProgress(iProgressPercentage);
                        }
                        streamLocal.Close();
                    }
                    streamRemote.Close();
                }
            }

            string zipToUnpack = romDir + @"\tmp.zip";
            ExtractFileToDirectory(zipToUnpack, romDir);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            button1.Enabled = true;
            string romDir = Application.StartupPath + @"\roms\" + treeView1.SelectedNode.Parent.Parent.Text;
            if (!Directory.Exists(romDir))
                Directory.CreateDirectory(romDir);

            string[] dirs = Directory.GetFiles(romDir, treeView1.SelectedNode.Text + @".*");

            if (dirs == null || dirs.Length == 0) //begin the download
                button1.Text = "DOWNLOAD && PLAY";
            else
                button1.Text = "PLAY GAME!";
            this.UseWaitCursor = false;
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button1.Enabled = true;
            button1.PerformClick(); //play
        }

        private void retroArchSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings settings = new settings();
            settings.Show();
            settings.TopMost = true;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about about = new about();
            about.Show();
            about.TopMost = true;
        }

        private void manageRetroArchFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void donateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start("http://newagesoldier.com/about/");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openRetroArchFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(RetroarchDir);
        }

        private void browseEmuSteamFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Cursor.Current = Cursors.Default;
            this.UseWaitCursor = false;
        }
    }
}
