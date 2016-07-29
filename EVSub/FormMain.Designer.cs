namespace EVSub
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.WMPMain = new AxWMPLib.AxWindowsMediaPlayer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rtbSub = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbWord = new System.Windows.Forms.TextBox();
            this.butTranslate = new System.Windows.Forms.Button();
            this.rtbMean = new System.Windows.Forms.RichTextBox();
            this.toolTipTranslate = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.WMPMain)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // WMPMain
            // 
            this.WMPMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WMPMain.Enabled = true;
            this.WMPMain.Location = new System.Drawing.Point(0, 0);
            this.WMPMain.Margin = new System.Windows.Forms.Padding(2);
            this.WMPMain.Name = "WMPMain";
            this.WMPMain.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("WMPMain.OcxState")));
            this.WMPMain.Size = new System.Drawing.Size(550, 411);
            this.WMPMain.TabIndex = 0;
            this.WMPMain.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.WMPMain_PlayStateChange);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(232, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(48, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.WMPMain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.menuStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(784, 411);
            this.splitContainer1.SplitterDistance = 550;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 2;
            // 
            // rtbSub
            // 
            this.rtbSub.BackColor = System.Drawing.SystemColors.Control;
            this.rtbSub.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbSub.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbSub.Location = new System.Drawing.Point(0, 0);
            this.rtbSub.Name = "rtbSub";
            this.rtbSub.ReadOnly = true;
            this.rtbSub.Size = new System.Drawing.Size(232, 250);
            this.rtbSub.TabIndex = 2;
            this.rtbSub.Text = "";
            this.rtbSub.WordWrap = false;
            this.rtbSub.SelectionChanged += new System.EventHandler(this.rtbSub_SelectionChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbWord);
            this.panel1.Controls.Add(this.butTranslate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(232, 20);
            this.panel1.TabIndex = 6;
            // 
            // tbWord
            // 
            this.tbWord.BackColor = System.Drawing.SystemColors.Control;
            this.tbWord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbWord.Location = new System.Drawing.Point(0, 0);
            this.tbWord.Margin = new System.Windows.Forms.Padding(0);
            this.tbWord.Name = "tbWord";
            this.tbWord.Size = new System.Drawing.Size(202, 20);
            this.tbWord.TabIndex = 3;
            // 
            // butTranslate
            // 
            this.butTranslate.Dock = System.Windows.Forms.DockStyle.Right;
            this.butTranslate.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butTranslate.Location = new System.Drawing.Point(202, 0);
            this.butTranslate.Margin = new System.Windows.Forms.Padding(0);
            this.butTranslate.Name = "butTranslate";
            this.butTranslate.Size = new System.Drawing.Size(30, 20);
            this.butTranslate.TabIndex = 5;
            this.butTranslate.Text = "→";
            this.butTranslate.UseVisualStyleBackColor = true;
            this.butTranslate.Click += new System.EventHandler(this.butTranslate_Click);
            // 
            // rtbMean
            // 
            this.rtbMean.BackColor = System.Drawing.SystemColors.Control;
            this.rtbMean.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbMean.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbMean.Location = new System.Drawing.Point(0, 20);
            this.rtbMean.Name = "rtbMean";
            this.rtbMean.ReadOnly = true;
            this.rtbMean.Size = new System.Drawing.Size(232, 113);
            this.rtbMean.TabIndex = 4;
            this.rtbMean.Text = "";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 24);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.rtbSub);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.rtbMean);
            this.splitContainer2.Panel2.Controls.Add(this.panel1);
            this.splitContainer2.Size = new System.Drawing.Size(232, 387);
            this.splitContainer2.SplitterDistance = 250;
            this.splitContainer2.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "EVSub";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.WMPMain)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer WMPMain;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTipTranslate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox rtbSub;
        private System.Windows.Forms.TextBox tbWord;
        private System.Windows.Forms.RichTextBox rtbMean;
        private System.Windows.Forms.Button butTranslate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer2;
    }
}

