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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace EmuSteam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string RetroarchDir = Application.StartupPath + @"\retroarch";
        private string RetroCores = Application.StartupPath + @"\retroarch\cores";
        public string currentRomName = "";
        public string currentSystem = "";
        public string currentRomLink = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            string masterRomDir = Application.StartupPath + @"\roms";
            if (!Directory.Exists(RetroarchDir))
            {
                Form2 form2 = new Form2();
                form2.Show(this);
            }
            if (!Directory.Exists(masterRomDir))
                Directory.CreateDirectory(masterRomDir);
            string retroarchDir = Application.StartupPath + @"\configs";
            if (!Directory.Exists(retroarchDir))
                Directory.CreateDirectory(retroarchDir);

            ListDirectory(treeView1, Application.StartupPath + @"\roms\");
            treeView1.ExpandAll();
        }

        private static string RemoveExt(string file)
        {
            int fileExtPos = file.LastIndexOf(".");
            if (fileExtPos >= 0)
                file = file.Substring(0, fileExtPos);
            return file;
        }

        private static bool checkSaveFiles(string fileName)
        {
            if (fileName.Contains(".srm"))
                return true;
            else
                return false;
        }

        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();

            var stack = new Stack<TreeNode>();
            var rootDirectory = new DirectoryInfo(path);
            var node = new TreeNode(rootDirectory.Name) { Tag = rootDirectory };
            stack.Push(node);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                var directoryInfo = (DirectoryInfo)currentNode.Tag;
                foreach (var directory in directoryInfo.GetDirectories())
                {
                    addControllers(directory.Name);
                    var childDirectoryNode = new TreeNode(directory.Name) { Tag = directory };
                    currentNode.Nodes.Add(childDirectoryNode);
                    stack.Push(childDirectoryNode);
                }
                foreach (var file in directoryInfo.GetFiles()){
                    string fileName = RemoveExt(file.Name);

                    if (!checkSaveFiles(file.Name)) //skip save files
                        currentNode.Nodes.Add(new TreeNode(fileName));
                }
            }

            treeView.Nodes.Add(node);
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

        private void addControllers(string console)
        {
            ToolStripMenuItem item = new ToolStripMenuItem("CMainMenu");
            item.Text = console;
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

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                loadingLabel.Visible = true;
                webBrowser1.Visible = false;
                string gameName = e.Node.Text;
                string platName = e.Node.Parent.Text;
                string navURL = "http://newagesoldier.com/myfiles/emustream/rom/" + platName + "/" + gameName;
                webBrowser1.Navigate(new Uri(navURL));
            }
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            if (dlprogressLabel.InvokeRequired)
                dlprogressLabel.Invoke(new MethodInvoker(delegate
                {
                    dlprogressLabel.Text = e.ProgressPercentage.ToString() + "%";
                }));
        }

        public void ExtractFileToDirectory(string zipFileName, string outputDirectory)
        {
            if (dlprogressLabel.InvokeRequired)
                dlprogressLabel.Invoke(new MethodInvoker(delegate
                {
                    dlprogressLabel.Text = "Unzipping files...";
                }));
            FileStream fs = File.OpenRead(zipFileName);
            string tmpFile = "";
            if (zipFileName.Contains(".zip") == true)
            {
                ZipFile zip = ZipFile.Read(fs);
                foreach (ZipEntry e in zip)
                {
                    e.Extract(outputDirectory, ExtractExistingFileAction.OverwriteSilently);
                }
                tmpFile = outputDirectory + @"\tmp.zip";
            }
            Directory.CreateDirectory(outputDirectory);
            
            fs.Close();

            File.Delete(tmpFile);
            backgroundWorker2.CancelAsync();
            return;
        }

        private string coreDetect(string console)
        {
            //all work on libretro (http://www.libretro.com/index.php/ecosystem/)
            if (console.Contains("arcade"))
                return "mame_libretro";
            else if (console.Contains("gameboy"))
                return "gambatte_libretro";
            else if (console.Contains("gba"))
                return "mednafen_gba_libretro";
            else if (console.Contains("n64"))
                return "mupen64plus_libretro";
            else if (console.Contains("nds"))
                return "desmume_libretro";
            else if (console.Contains("neogeo"))
                return "mednafen_ngp_libretro";
            else if (console.Contains("atari"))
                return "stella_libretro";
            else if (console.Contains("lynx"))
                return "handy_libretro";
            else if (console.Contains("psx"))
                return "mednafen_psx_libretro";
            else if (console.Contains("snes"))
                return "snes9x_libretro";
            else if (console.Contains("nes"))
                return "nestopia_libretro";
            else if (console.Contains("sega"))
                return "genesis_plus_gx_libretro";
            else
                return "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] argArray = { currentSystem, currentRomLink }; //dir, URL

            string romDir = Application.StartupPath + @"\roms\" + currentSystem;
            if (!Directory.Exists(romDir))
                Directory.CreateDirectory(romDir);

            string[] dirs = Directory.GetFiles(romDir, currentRomName + @".*");

            if (dirs == null || dirs.Length == 0) //begin the download
            {
                dlprogressLabel.Visible = true;
                button1.Enabled = false;
                if (!backgroundWorker2.IsBusy)
                    backgroundWorker2.RunWorkerAsync(argArray);
            }
            else
            {
                button1.Text = "PLAY GAME!";

                string strCmdText = Application.StartupPath + @"\retroarch\retroarch.exe";
                string fs = "";
                if (Properties.Settings.Default.fullscreen == true)
                    fs = " -f ";
                mergeFiles(/*treeView1.SelectedNode.Parent.Parent.Text*/currentSystem);
                string confFile = "--appendconfig " + '"' + Application.StartupPath + @"\configs\" + /*treeView1.SelectedNode.Parent.Parent.Text*/currentSystem + " merge.cfg" + '"' + " ";

                string arguments = @"-L " + Application.StartupPath + @"\retroarch\cores\" + coreDetect(/*treeView1.SelectedNode.Parent.Parent.Text*/currentSystem) + ".dll " + fs + confFile + " " + '"' + dirs[0] + '"';
                try
                {
                    Process.Start(strCmdText, arguments);
                }
                catch
                {
                    MessageBox.Show("RetroArch issue with starting.");
                }
            }
        }

        static String BytesToString(double byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs((long)byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
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

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] args = (string[])e.Argument;
            string romDir = Application.StartupPath + @"\roms\" + args[0];
            string realURL = HttpUtility.HtmlDecode(args[1]);

            string sUrlToReadFileFrom = realURL;
            string sFilePathToWriteFileTo = "";

            sFilePathToWriteFileTo = romDir + @"\tmp.zip";

            Uri url = new Uri(sUrlToReadFileFrom);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Close();
            long iSize = response.ContentLength;
            Int64 iRunningByteTotal = 0;

            using (WebClient DLclient = new WebClient())
            {
                using (Stream streamRemote = DLclient.OpenRead(new Uri(sUrlToReadFileFrom)))
                {
                    using (Stream streamLocal = new FileStream(sFilePathToWriteFileTo, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        int iByteSize = 0;
                        byte[] byteBuffer;
                        byteBuffer = new byte[iSize];
                        while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                        {
                            streamLocal.Write(byteBuffer, 0, iByteSize);
                            iRunningByteTotal += iByteSize;

                            if (backgroundWorker2.CancellationPending == true)
                            {
                                e.Cancel = true;
                                break;
                            }

                            double dIndex = (double)(iRunningByteTotal);
                            double dTotal = (double)byteBuffer.Length;
                            double dProgressPercentage = (dIndex / dTotal);
                            int iProgressPercentage = (int)(dProgressPercentage * 100);
                            if (dIndex > 0 && dTotal > 0)
                            {
                                if (dlprogressLabel.InvokeRequired)
                                    dlprogressLabel.Invoke(new MethodInvoker(delegate
                                    {
                                        dlprogressLabel.Text = BytesToString(dIndex).ToString() + "/" + BytesToString(dTotal).ToString() + " (" + iProgressPercentage.ToString() + "%)";
                                    }));
                            }
                            backgroundWorker2.ReportProgress(iProgressPercentage);
                        }
                        streamLocal.Close();
                    }
                    streamRemote.Close();
                }
            }

            ExtractFileToDirectory(sFilePathToWriteFileTo, romDir);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser1.Visible = true;
            loadingLabel.Visible = false;
            WebClient w = new WebClient();

            try
            {
                if (webBrowser1.Url.ToString().Contains("http://newagesoldier.com/myfiles/emustream/rom/") == true)
                {
                    currentRomName = webBrowser1.Url.ToString().Split(new char[] { '/', '/' })[7];
                    currentSystem = webBrowser1.Url.ToString().Split(new char[] { '/', '/' })[6];
                    string s = w.DownloadString(webBrowser1.Url.ToString());
                    currentRomLink = DLLinkFinder.Find(s)[0].ToString(); //grab our DL link
                    button1.Enabled = true;
                }
                else
                    button1.Enabled = false;
            }
            catch
            {
            }

            string romDir = Application.StartupPath + @"\roms\" + currentSystem;
            if (!Directory.Exists(romDir))
                Directory.CreateDirectory(romDir);

            string[] dirs = Directory.GetFiles(romDir, currentRomName + @".*");

            if (dirs == null || dirs.Length == 0) //begin the download
                button1.Text = "DOWNLOAD && PLAY";
            else
                button1.Text = "PLAY GAME!";
            this.UseWaitCursor = false;
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button1.Enabled = true;
            progressBar1.Value = 0;
            dlprogressLabel.Visible = false;
            button1.Text = "PLAY GAME!";
            button1.PerformClick(); //play
            ListDirectory(treeView1, Application.StartupPath + @"\roms\");
            treeView1.ExpandAll();
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
            if (!File.Exists("configs/defaults.cfg"))
                using (File.Create("configs/defaults.cfg"));
        }

        public struct LinkItem
        {
            public string Href;
            public string Text;

            public override string ToString()
            {
                return Href + "\n\t" + Text;
            }
        }

        static class DLLinkFinder
        {
            public static List<LinkItem> Find(string file)
            {
                List<LinkItem> list = new List<LinkItem>();

                MatchCollection m1 = Regex.Matches(file, @"(href=\""http://erfg1234.byethost7.com/.*?>.*?</a>)",
                    RegexOptions.Singleline);

                foreach (Match m in m1)
                {
                    string value = m.Groups[1].Value;
                    LinkItem i = new LinkItem();

                    Match m2 = Regex.Match(value, @"href=\""(.*?)\""",
                    RegexOptions.Singleline);
                    if (m2.Success)
                    {
                        i.Href = m2.Groups[1].Value;
                    }

                    list.Add(i);
                }
                return list;
            }
        }

        private void configureJoysticksToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
