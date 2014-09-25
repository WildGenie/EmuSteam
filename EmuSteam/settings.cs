using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmuSteam
{
    public partial class settings : Form
    {
        public settings()
        {
            InitializeComponent();
        }

        private void settings_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Properties.Settings.Default.fullscreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.fullscreen = checkBox1.Checked;
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
