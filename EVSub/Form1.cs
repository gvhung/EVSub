using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EVSub
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "All Videos Files |*.wmv; *.avi; *.flv; *.mkv; *.mp4; *.ts; *.webm|All Files|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Debug.WriteLine("File = " + openFileDialog1.FileName);
                WMPMain.URL = openFileDialog1.FileName;
                WMPMain.Ctlcontrols.play();
            }
        }
    }
}
