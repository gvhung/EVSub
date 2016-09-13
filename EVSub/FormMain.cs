using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using WMPLib;
using SubtitlesParser;
using System.Drawing;
using System.Text.RegularExpressions;

namespace EVSub
{
    public partial class FormMain : Form
    {
        private List<SubtitleItem> subItems;//Danh sách sub
        private int[] subFirstLine;//Lưu STT dòng đầu tiên của mỗi sub, phần tử cuối lưu tổng số dòng của sub
        private int currentSubIndex;//Lưu hiện tại đang là sub thứ bao nhiêu
        private Font regularFont = new Font("Tahoma", 12, FontStyle.Regular);
        private Font boldFont = new Font("Tahoma", 12, FontStyle.Bold);

        private EVDictionary evdic = new EVDictionary();//Từ điểm anh việt
        private bool rtbSubSystemUpdating = false;//Dùng để phân biệt select của hệ thông và người dùng.
        private int boldStartSelect;//Lưu đoạn đang được bôi đậm
        private int boldEndSelect;//Lưu đoạn đang được bôi đậm

        private float timeOnArrowKeyDown = 5.0f;//Số giây tua khi phím mũi tên được nhấn

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            openVideoDialog.Filter = "All Videos Files |*.wmv; *.avi; *.flv; *.mkv; *.mp4; *.ts; *.webm|All Files|*.*";
            openVideoDialog.FilterIndex = 1;
            openVideoDialog.Multiselect = false;
            openSubDialog.Filter = "All Subtitles Files |*.srt; *.ass|All Files|*.*";
            openSubDialog.FilterIndex = 1;
            openSubDialog.Multiselect = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showDialogOpenVideo();
        }

        private void WMPMain_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (WMPMain.playState == WMPPlayState.wmppsPlaying || WMPMain.playState == WMPPlayState.wmppsPaused)
            {
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }
        }

        private void rtbSub_SelectionChanged(object sender, EventArgs e)
        {
            string word = Regex.Replace(rtbSub.SelectedText, "[^a-zA-Z]+", "");
            word = word.ToLower();
            if (rtbSubSystemUpdating == false && word.Length > 0)
            {

                string mean = evdic.translateWithRemoveEnding(word);
                if (mean != null)
                {
                    tbWord.Text = word;
                    rtbMean.Text = mean;
                }
                else
                {
                    tbWord.Text = rtbSub.SelectedText;
                }
            }
        }

        private void butTranslate_Click(object sender, EventArgs e)
        {
            TranslatetbWord();
        }

        private void tbWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TranslatetbWord();
            }
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.SuppressKeyPress = true;
            switch (e.KeyCode)
            {
                case Keys.Space:
                    changePlayOrPauseVideo();
                    break;
                case Keys.Left:
                case Keys.A:
                    if (WMPMain.Ctlcontrols.currentPosition > timeOnArrowKeyDown)
                    {
                        WMPMain.Ctlcontrols.currentPosition -= timeOnArrowKeyDown;
                    }
                    break;
                case Keys.Right:
                case Keys.D:
                    if (WMPMain.currentMedia.duration - WMPMain.Ctlcontrols.currentPosition > timeOnArrowKeyDown)
                    {
                        WMPMain.Ctlcontrols.currentPosition += timeOnArrowKeyDown;
                    }
                    break;
                case Keys.Up:
                case Keys.W:
                    if (currentSubIndex - 1 >= 0)
                    {
                        WMPMain.Ctlcontrols.currentPosition = subItems[currentSubIndex - 1].StartTime / 1000.0;
                    }
                    break;
                case Keys.Down:
                case Keys.S:
                    if (currentSubIndex + 1 < subItems.Count)
                    {
                        WMPMain.Ctlcontrols.currentPosition = subItems[currentSubIndex + 1].StartTime / 1000.0;
                    }
                    break;
                case Keys.O:
                    if (e.Control)
                    {
                        showDialogOpenVideo();
                    }
                    break;
            }
        }

        private void WMPMain_KeyDownEvent(object sender, AxWMPLib._WMPOCXEvents_KeyDownEvent e)
        {
            switch (e.nKeyCode)
            {
                case 32://SPACE                    
                    onKeyDown(sender, new KeyEventArgs(Keys.Space));
                    break;
                case 38://UP ARROW
                case (short)Keys.W:
                    onKeyDown(sender, new KeyEventArgs(Keys.Up));
                    break;
                case 40://DOWN ARROW
                case (short)Keys.S:
                    onKeyDown(sender, new KeyEventArgs(Keys.Down));
                    break;
                case 37://LEFT ARROW
                case (short)Keys.A:
                    onKeyDown(sender, new KeyEventArgs(Keys.Left));
                    break;
                case 39://RIGHT ARROW
                case (short)Keys.D:
                    onKeyDown(sender, new KeyEventArgs(Keys.Right));
                    break;
                case (short)Keys.O://Ctrl + O
                    if (e.nShiftState == 2)
                    {
                        onKeyDown(sender, new KeyEventArgs(Keys.Control | Keys.O));
                    }
                    break;
            }
        }

        private void loadSubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openSubDialog.ShowDialog() == DialogResult.OK)
            {
                loadSub(openSubDialog.FileName);
            }
        }

        /// <summary>
        /// Đếm thời gian để thay đổi sub
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            int cp = Convert.ToInt32(WMPMain.Ctlcontrols.currentPosition * 1000);
            if (cp < subItems[0].StartTime
                || cp > subItems[subItems.Count - 1].EndTime)
            {
                return;
            }
            if (currentSubIndex + 1 < subItems.Count)
            {
                if (cp >= subItems[currentSubIndex].StartTime
                    && cp < subItems[currentSubIndex + 1].StartTime)
                {
                    return;
                }
                if (cp >= subItems[currentSubIndex + 1].StartTime
                    && cp <= subItems[currentSubIndex + 1].EndTime)
                {
                    currentSubIndex++;
                    boldCurrentSub();
                    return;
                }
            }
            //Tìm sub nếu như tua một đoạn
            //Debug.WriteLine("Find sub = " + WMPMain.Ctlcontrols.currentPosition + " " + subItems[currentSubIndex].StartTime / 1000.0 + " " + currentSubIndex);
            SubtitleItem searchItem = new SubtitleItem();
            searchItem.StartTime = cp;
            int index = ~subItems.BinarySearch(searchItem, new StartTimeComparer());//index = phần tử liền sau, index có thể = count
            index = Math.Abs(index);
            //Debug.WriteLine("Find sub = " + index);
            if (index > 0)
            {
                index--;//Do index = phần tử liền sau
                if (index + 1 < subItems.Count)
                {
                    if (cp >= subItems[index].StartTime
                        && cp < subItems[index + 1].StartTime)
                    {
                        currentSubIndex = index;
                        boldCurrentSub();
                    }
                }
                else
                {
                    currentSubIndex = subItems.Count - 1;
                    boldCurrentSub();
                }
            }
        }

        /// <summary>
        /// Khi video play chuyển sang pause và ngược lại
        /// </summary>
        private void changePlayOrPauseVideo()
        {
            if (WMPMain.playState == WMPPlayState.wmppsPlaying)
            {
                WMPMain.Ctlcontrols.pause();
            }
            else
            {
                WMPMain.Ctlcontrols.play();
            }
        }

        /// <summary>
        /// Dịch từ trong ô tbWord
        /// </summary>
        private void TranslatetbWord()
        {
            string word = tbWord.Text.ToLower();
            word = word.Trim();
            if (word.Length > 0)
            {
                string mean = evdic.translateWithRemoveEnding(word);
                if (mean == null)
                {
                    mean = evdic.GoogleTranslate2(word);
                }
                if (mean != null)
                {
                    tbWord.Text = word;
                    rtbMean.Text = mean;
                }
            }
        }

        /// <summary>
        /// In đậm sub hiện tại, bỏ in đậm sub trước
        /// </summary>
        private void boldCurrentSub()
        {
            rtbSubSystemUpdating = true;
            rtbSub.Select(boldStartSelect, boldEndSelect - boldStartSelect);
            rtbSub.SelectionFont = regularFont;
            boldStartSelect = rtbSub.GetFirstCharIndexFromLine(subFirstLine[currentSubIndex]);
            boldEndSelect = rtbSub.GetFirstCharIndexFromLine(subFirstLine[currentSubIndex + 1]);
            rtbSub.Select(boldStartSelect, boldEndSelect - boldStartSelect);
            rtbSub.SelectionFont = boldFont;
            if (currentSubIndex > subItems.Count - 2) { }
            else if (currentSubIndex > 5)
            {
                rtbSub.Select(rtbSub.GetFirstCharIndexFromLine(subFirstLine[currentSubIndex - 5]), 0);
                rtbSub.ScrollToCaret();
            }
            else
            {
                rtbSub.Select(0, 0);
                rtbSub.ScrollToCaret();
            }
            rtbSubSystemUpdating = false;
        }

        /// <summary>
        /// Đọc phụ đề
        /// </summary>
        /// <param name="URLSubFile"></param>
        private bool loadSub(string URLSubFile)
        {
            rtbSub.Text = "";
            var parser = new SubtitlesParser.Parsers.SubParser();
            Debug.WriteLine("File Sub = " + URLSubFile);
            try
            {
                using (var fileStream = File.OpenRead(URLSubFile))
                {
                    subItems = parser.ParseStream(fileStream);
                    subFirstLine = new int[subItems.Count + 1];
                    subFirstLine[0] = 0;
                    for (int i = 1; i <= subItems.Count; i++)
                    {
                        subFirstLine[i] = subFirstLine[i - 1] + subItems[i - 1].Lines.Count + 1;//Cộng 1 do các dòng sub cách nhau một dòng trắng
                    }
                    string allSub = string.Join(Environment.NewLine + Environment.NewLine, subItems);
                    allSub += Environment.NewLine + Environment.NewLine;//Thêm 1 dòng nữa cho dễ nhìn và để phần tử cuối của subFirstLine vẫn đúng
                    rtbSub.Font = regularFont;
                    rtbSub.Text = allSub;
                    currentSubIndex = 0;
                    boldCurrentSub();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Load sub error = " + ex);
                rtbSub.Text = "CCG";
                return false;
            }
        }

        /// <summary>
        /// Đọc phụ đề, tùy chọn loại phụ đề
        /// </summary>
        /// <param name="URLVideoFile"></param>
        /// <param name="subType"></param>
        /// <returns></returns>
        private bool loadSub(string URLVideoFile, string subType)
        {
            string pathSubFile = Path.GetDirectoryName(URLVideoFile) + @"\" + Path.GetFileNameWithoutExtension(URLVideoFile) + "." + subType;
            return loadSub(pathSubFile);
        }

        private void showDialogOpenVideo()
        {
            if (openVideoDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Debug.WriteLine("File video = " + openVideoDialog.FileName);
                    WMPMain.URL = openVideoDialog.FileName;
                    WMPMain.Ctlcontrols.play();

                    this.Text = "EVSub - " + Path.GetFileName(openVideoDialog.FileName);
                }
                catch (Exception ex)
                {
                    this.Text = "EVSub";
                    MessageBox.Show("Can not open this file, error = " + ex);
                }
                if (!loadSub(WMPMain.URL, "srt"))
                {
                    loadSub(WMPMain.URL, "ass");
                }
            }
        }
    }
}
