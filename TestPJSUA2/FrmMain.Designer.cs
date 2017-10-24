namespace TestPJSUA2
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxSpeaker = new System.Windows.Forms.CheckBox();
            this.cbxRight = new System.Windows.Forms.CheckBox();
            this.cbxLeft = new System.Windows.Forms.CheckBox();
            this.lblHeadset = new System.Windows.Forms.Label();
            this.lblPtt = new System.Windows.Forms.Label();
            this.btnHangup = new System.Windows.Forms.Button();
            this.lblCallstackCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxAccount = new System.Windows.Forms.ComboBox();
            this.btnCall = new System.Windows.Forms.Button();
            this.btnAnswer = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.TextBox();
            this.timerSIPMessages = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.lblHeadset);
            this.panel1.Controls.Add(this.lblPtt);
            this.panel1.Controls.Add(this.btnHangup);
            this.panel1.Controls.Add(this.lblCallstackCount);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbxAccount);
            this.panel1.Controls.Add(this.btnCall);
            this.panel1.Controls.Add(this.btnAnswer);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.MinimumSize = new System.Drawing.Size(2, 147);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(606, 198);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxSpeaker);
            this.groupBox1.Controls.Add(this.cbxRight);
            this.groupBox1.Controls.Add(this.cbxLeft);
            this.groupBox1.Location = new System.Drawing.Point(356, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(80, 80);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Channels";
            // 
            // cbxSpeaker
            // 
            this.cbxSpeaker.AutoSize = true;
            this.cbxSpeaker.Location = new System.Drawing.Point(10, 63);
            this.cbxSpeaker.Margin = new System.Windows.Forms.Padding(2);
            this.cbxSpeaker.Name = "cbxSpeaker";
            this.cbxSpeaker.Size = new System.Drawing.Size(64, 17);
            this.cbxSpeaker.TabIndex = 2;
            this.cbxSpeaker.Text = "speaker";
            this.cbxSpeaker.UseVisualStyleBackColor = true;
            // 
            // cbxRight
            // 
            this.cbxRight.AutoSize = true;
            this.cbxRight.Checked = true;
            this.cbxRight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxRight.Location = new System.Drawing.Point(10, 42);
            this.cbxRight.Margin = new System.Windows.Forms.Padding(2);
            this.cbxRight.Name = "cbxRight";
            this.cbxRight.Size = new System.Drawing.Size(46, 17);
            this.cbxRight.TabIndex = 1;
            this.cbxRight.Text = "right";
            this.cbxRight.UseVisualStyleBackColor = true;
            // 
            // cbxLeft
            // 
            this.cbxLeft.AutoSize = true;
            this.cbxLeft.Checked = true;
            this.cbxLeft.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxLeft.Location = new System.Drawing.Point(10, 20);
            this.cbxLeft.Margin = new System.Windows.Forms.Padding(2);
            this.cbxLeft.Name = "cbxLeft";
            this.cbxLeft.Size = new System.Drawing.Size(40, 17);
            this.cbxLeft.TabIndex = 0;
            this.cbxLeft.Text = "left";
            this.cbxLeft.UseVisualStyleBackColor = true;
            this.cbxLeft.CheckedChanged += new System.EventHandler(this.cbxLeft_CheckedChanged);
            // 
            // lblHeadset
            // 
            this.lblHeadset.AutoSize = true;
            this.lblHeadset.Location = new System.Drawing.Point(294, 72);
            this.lblHeadset.Name = "lblHeadset";
            this.lblHeadset.Size = new System.Drawing.Size(16, 13);
            this.lblHeadset.TabIndex = 10;
            this.lblHeadset.Text = "---";
            // 
            // lblPtt
            // 
            this.lblPtt.AutoSize = true;
            this.lblPtt.Location = new System.Drawing.Point(294, 59);
            this.lblPtt.Name = "lblPtt";
            this.lblPtt.Size = new System.Drawing.Size(16, 13);
            this.lblPtt.TabIndex = 9;
            this.lblPtt.Text = "---";
            // 
            // btnHangup
            // 
            this.btnHangup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnHangup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHangup.ForeColor = System.Drawing.Color.White;
            this.btnHangup.Location = new System.Drawing.Point(179, 72);
            this.btnHangup.Margin = new System.Windows.Forms.Padding(2);
            this.btnHangup.Name = "btnHangup";
            this.btnHangup.Size = new System.Drawing.Size(74, 57);
            this.btnHangup.TabIndex = 7;
            this.btnHangup.Text = "Op hangen";
            this.btnHangup.UseVisualStyleBackColor = false;
            this.btnHangup.Visible = false;
            this.btnHangup.Click += new System.EventHandler(this.btnHangup_Click);
            // 
            // lblCallstackCount
            // 
            this.lblCallstackCount.AutoSize = true;
            this.lblCallstackCount.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCallstackCount.Location = new System.Drawing.Point(483, 153);
            this.lblCallstackCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCallstackCount.Name = "lblCallstackCount";
            this.lblCallstackCount.Size = new System.Drawing.Size(23, 17);
            this.lblCallstackCount.TabIndex = 6;
            this.lblCallstackCount.Text = "...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(370, 155);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Callstack count:";
            // 
            // cbxAccount
            // 
            this.cbxAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAccount.FormattingEnabled = true;
            this.cbxAccount.Items.AddRange(new object[] {
            "12345",
            "10000",
            "1001",
            "1003",
            "1013",
            "INTERCOM_CUB_X",
            "I_ESL_SET_NOISE_LEVEL",
            "RIGHTConference_Pos02",
            "LEFTConference_Pos01001",
            "RIGHTConference_Pos01001",
            "LEFTConference_Pos01002",
            "RIGHTConference_Pos01002"});
            this.cbxAccount.Location = new System.Drawing.Point(88, 18);
            this.cbxAccount.Margin = new System.Windows.Forms.Padding(2);
            this.cbxAccount.Name = "cbxAccount";
            this.cbxAccount.Size = new System.Drawing.Size(222, 24);
            this.cbxAccount.TabIndex = 4;
            // 
            // btnCall
            // 
            this.btnCall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCall.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCall.ForeColor = System.Drawing.Color.White;
            this.btnCall.Location = new System.Drawing.Point(8, 18);
            this.btnCall.Margin = new System.Windows.Forms.Padding(2);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(66, 57);
            this.btnCall.TabIndex = 3;
            this.btnCall.Text = "Call";
            this.btnCall.UseVisualStyleBackColor = false;
            this.btnCall.Click += new System.EventHandler(this.btnCall_Click);
            // 
            // btnAnswer
            // 
            this.btnAnswer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAnswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnswer.ForeColor = System.Drawing.Color.White;
            this.btnAnswer.Location = new System.Drawing.Point(88, 72);
            this.btnAnswer.Margin = new System.Windows.Forms.Padding(2);
            this.btnAnswer.Name = "btnAnswer";
            this.btnAnswer.Size = new System.Drawing.Size(74, 57);
            this.btnAnswer.TabIndex = 2;
            this.btnAnswer.Text = "Opnemen";
            this.btnAnswer.UseVisualStyleBackColor = false;
            this.btnAnswer.Visible = false;
            this.btnAnswer.Click += new System.EventHandler(this.btnAnswer_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.DarkRed;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(530, 18);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 57);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listBox1.Location = new System.Drawing.Point(0, 198);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2);
            this.listBox1.Multiline = true;
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(606, 377);
            this.listBox1.TabIndex = 3;
            // 
            // timerSIPMessages
            // 
            this.timerSIPMessages.Interval = 500;
            this.timerSIPMessages.Tick += new System.EventHandler(this.timerSIPMessages_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 587);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(606, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 609);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test PJSUA2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAnswer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCall;
        private System.Windows.Forms.TextBox listBox1;
        private System.Windows.Forms.ComboBox cbxAccount;
        private System.Windows.Forms.Timer timerSIPMessages;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label lblCallstackCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnHangup;
        private System.Windows.Forms.Label lblHeadset;
        private System.Windows.Forms.Label lblPtt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbxSpeaker;
        private System.Windows.Forms.CheckBox cbxRight;
        private System.Windows.Forms.CheckBox cbxLeft;
    }
}

