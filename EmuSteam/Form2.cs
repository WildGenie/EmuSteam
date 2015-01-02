﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Web;
using System.Net;
using Ionic.Zip;

namespace EmuSteam
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private string retroarchDir = Application.StartupPath + @"\retroarch\";
        private string retroCores = Application.StartupPath + @"\retroarch\cores";

        private void getRA(string architecture)
        {
            progressBar1.Value = 0;
            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync(architecture);
            statusLabel.Text = "STATUS: Download files...";
            statusLabel.Visible = true;
            progressBar1.Visible = true;
            if (architecture.Contains("core") == true)
                button4.Enabled = false;
            else
                button3.Enabled = false;
        }

        /*private void button1_Click(object sender, EventArgs e) //get 32 bit 
        {
            //((Form1)Owner).getRetroArch();
            //this.Close();
            getRA("32");
        }*/

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ExtractFileToDirectory(string zipFileName, string outputDirectory)
        {
            if (statusLabel.InvokeRequired)
                statusLabel.Invoke(new MethodInvoker(delegate
                {
                    statusLabel.Text = "STATUS: Unzipping files. Please wait...";
                }));
            FileStream fs = File.OpenRead(zipFileName);
            ZipFile zip = ZipFile.Read(fs);
            Directory.CreateDirectory(outputDirectory);
            foreach (ZipEntry e in zip)
            {
                e.Extract(outputDirectory, ExtractExistingFileAction.OverwriteSilently);
            }
            fs.Close();

            string tmpFile = outputDirectory + @"\tmp.zip";
            File.Delete(tmpFile);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string retroarchDir = Application.StartupPath + @"\retroarch";
            if (!Directory.Exists(retroarchDir))
                Directory.CreateDirectory(retroarchDir);
            string dynamicURL = e.Argument as string;
            string realURL = HttpUtility.HtmlDecode("http://newagesoldier.com/myfiles/xml/emusteam/getra.php?v=" + e.Argument);

            string coreDir = Application.StartupPath + @"\retroarch\cores";
            if (!Directory.Exists(coreDir))
                Directory.CreateDirectory(coreDir);

            string sUrlToReadFileFrom = realURL;
            string sFilePathToWriteFileTo = retroarchDir + @"\tmp.zip";

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
                                backgroundWorker1.ReportProgress(iProgressPercentage);
                        }
                        streamLocal.Close();
                    }
                    streamRemote.Close();
                }
            }

            string zipToUnpack = retroarchDir + @"\tmp.zip";
            if (dynamicURL.Contains("core") == true)
                ExtractFileToDirectory(zipToUnpack, coreDir);
            else
                ExtractFileToDirectory(zipToUnpack, retroarchDir);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            progressBar1.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void retroArchFolderCheck()
        {
            if (Directory.Exists(retroarchDir))
            {
                button3.Enabled = false; //get btn
                button1.Enabled = true; //delete btn
            }
            else
            {
                button3.Enabled = true; //get btn
                button1.Enabled = false; //delete btn
                progressBar1.Visible = false;
                statusLabel.Visible = false;
            }

            if (Directory.Exists(retroCores))
            {
                button4.Enabled = false; //get btn
                button5.Enabled = true; //delete btn
            }
            else
            {
                button4.Enabled = true; //get btn
                button5.Enabled = false; //delete btn
                progressBar1.Visible = false;
                statusLabel.Visible = false;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            statusLabel.Text = "STATUS: COMPLETE!";
            retroArchFolderCheck();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            retroArchFolderCheck();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            getRA("64");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Directory.Delete(retroarchDir, true);
            retroArchFolderCheck();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            getRA("cores64");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Directory.Delete(retroCores, true);
            retroArchFolderCheck();
        }
    }
}
