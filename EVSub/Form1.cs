using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using WMPLib;
using SubtitlesParser;
using System.Drawing;

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
                Debug.WriteLine("File video = " + openFileDialog1.FileName);
                WMPMain.URL = openFileDialog1.FileName;
                WMPMain.Ctlcontrols.play();

                rtbSub.Text = "";
                var parser = new SubtitlesParser.Parsers.SubParser();
                string pathToSrtFile = Path.GetDirectoryName(WMPMain.URL) + @"\" + Path.GetFileNameWithoutExtension(WMPMain.URL) + ".srt";
                Debug.WriteLine("File Sub = " + pathToSrtFile);
                try
                {
                    using (var fileStream = File.OpenRead(pathToSrtFile))
                    {
                        subItems = parser.ParseStream(fileStream);
                        subFirstLine = new int[subItems.Count + 1];
                        subFirstLine[0] = 0;
                        for (int i = 1; i <= subItems.Count; i++)
                        {
                            subFirstLine[i] = subFirstLine[i - 1] + subItems[i - 1].Lines.Count + 1;
                        }
                        string allSub = string.Join(Environment.NewLine + Environment.NewLine, subItems);
                        rtbSub.Font = regularFont;
                        rtbSub.Text = allSub;
                        currentSubIndex = 0;
                        boldCurrentSub();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Load sub error = " + ex);
                    rtbSub.Text = "CCG";
                }
            }
        }

        private void WMPMain_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (WMPMain.playState == WMPPlayState.wmppsPlaying)
            {
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }
        }

        /// <summary>
        /// Đếm thời gian để thay đổi sub
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (WMPMain.Ctlcontrols.currentPosition > subItems[currentSubIndex].StartTime / 1000.0
                && WMPMain.Ctlcontrols.currentPosition < subItems[currentSubIndex].EndTime / 1000.0)
            {
                return;
            }
            if (WMPMain.Ctlcontrols.currentPosition > subItems[currentSubIndex + 1].StartTime / 1000.0
                && WMPMain.Ctlcontrols.currentPosition < subItems[currentSubIndex + 1].EndTime / 1000.0)
            {
                currentSubIndex++;
                boldCurrentSub();
                return;
            }
            SubtitleItem searchItem = new SubtitleItem();
            searchItem.StartTime = Convert.ToInt32(WMPMain.Ctlcontrols.currentPosition * 1000);
            int index = ~subItems.BinarySearch(searchItem, new StartTimeComparer());
            if (index <= 0)
            {
                return;
            }else
            {
                index--;
            }
            if (WMPMain.Ctlcontrols.currentPosition > subItems[index].StartTime / 1000.0
                && WMPMain.Ctlcontrols.currentPosition < subItems[index].EndTime / 1000.0)
            {
                currentSubIndex = index;
                boldCurrentSub();
                return;
            }
        }

        /// <summary>
        /// In đậm sub hiện tại, bỏ in đậm sub trước
        /// </summary>
        private void boldCurrentSub()
        {
            rtbSub.Select(boldStartSelect, boldEndSelect - boldStartSelect);
            rtbSub.SelectionFont = regularFont;
            boldStartSelect = rtbSub.GetFirstCharIndexFromLine(subFirstLine[currentSubIndex]);
            boldEndSelect = rtbSub.GetFirstCharIndexFromLine(subFirstLine[currentSubIndex + 1]);
            rtbSub.Select(boldStartSelect, boldEndSelect - boldStartSelect);
            rtbSub.SelectionFont = boldFont;
            if (currentSubIndex > 5)
            {
                rtbSub.Select(rtbSub.GetFirstCharIndexFromLine(subFirstLine[currentSubIndex - 5]), 0);
                rtbSub.ScrollToCaret();
            }
            else
            {
                rtbSub.Select(0, 0);
                rtbSub.ScrollToCaret();
            }
        }
        private List<SubtitleItem> subItems;//Danh sách sub
        private int[] subFirstLine;//Lưu STT dòng đầu tiên của mỗi sub, phần tử cuối lưu tổng số dòng của sub
        private int currentSubIndex;//Lưu hiện tại đang là sub thứ bao nhiêu
        private Font regularFont = new Font("Tahoma", 12, FontStyle.Regular);
        private Font boldFont = new Font("Tahoma", 12, FontStyle.Bold);

        private int boldStartSelect;//Lưu đoạn đang được bôi đậm
        private int boldEndSelect;//Lưu đoạn đang được bôi đậm
    }
}
