namespace StubSIM2UNET
{
    partial class FrmMainStub
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainStub));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSelectLogDir = new System.Windows.Forms.Button();
            this.tbxLog4NetDirectory = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxPort = new System.Windows.Forms.TextBox();
            this.cmbxInterval = new System.Windows.Forms.ComboBox();
            this.cbxInterval = new System.Windows.Forms.CheckBox();
            this.cbxUsePCAPTime = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPCap = new System.Windows.Forms.TrackBar();
            this.tbxDestination = new System.Windows.Forms.TextBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lbxPCAP = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sendThisPacketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPCap)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSelectLogDir);
            this.groupBox1.Controls.Add(this.tbxLog4NetDirectory);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(760, 126);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PCAP File";
            // 
            // btnSelectLogDir
            // 
            this.btnSelectLogDir.BackColor = System.Drawing.Color.Black;
            this.btnSelectLogDir.ForeColor = System.Drawing.Color.Transparent;
            this.btnSelectLogDir.Image = global::StubSIM2UNET.Properties.Resources.open_file_icon32;
            this.btnSelectLogDir.Location = new System.Drawing.Point(676, 38);
            this.btnSelectLogDir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelectLogDir.Name = "btnSelectLogDir";
            this.btnSelectLogDir.Size = new System.Drawing.Size(51, 50);
            this.btnSelectLogDir.TabIndex = 39;
            this.btnSelectLogDir.UseVisualStyleBackColor = false;
            this.btnSelectLogDir.Click += new System.EventHandler(this.btnSelectLogDir_Click);
            // 
            // tbxLog4NetDirectory
            // 
            this.tbxLog4NetDirectory.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tbxLog4NetDirectory.Location = new System.Drawing.Point(25, 52);
            this.tbxLog4NetDirectory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbxLog4NetDirectory.Name = "tbxLog4NetDirectory";
            this.tbxLog4NetDirectory.ReadOnly = true;
            this.tbxLog4NetDirectory.Size = new System.Drawing.Size(644, 22);
            this.tbxLog4NetDirectory.TabIndex = 38;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tbxPort);
            this.groupBox2.Controls.Add(this.cmbxInterval);
            this.groupBox2.Controls.Add(this.cbxInterval);
            this.groupBox2.Controls.Add(this.cbxUsePCAPTime);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbPCap);
            this.groupBox2.Controls.Add(this.tbxDestination);
            this.groupBox2.Controls.Add(this.btnPlay);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox2.Location = new System.Drawing.Point(16, 148);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(760, 126);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 89);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Port:";
            // 
            // tbxPort
            // 
            this.tbxPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPort.Location = new System.Drawing.Point(302, 87);
            this.tbxPort.Margin = new System.Windows.Forms.Padding(4);
            this.tbxPort.Name = "tbxPort";
            this.tbxPort.Size = new System.Drawing.Size(57, 27);
            this.tbxPort.TabIndex = 8;
            this.tbxPort.Text = "11000";
            // 
            // cmbxInterval
            // 
            this.cmbxInterval.FormattingEnabled = true;
            this.cmbxInterval.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "30",
            "60"});
            this.cmbxInterval.Location = new System.Drawing.Point(671, 85);
            this.cmbxInterval.Margin = new System.Windows.Forms.Padding(4);
            this.cmbxInterval.Name = "cmbxInterval";
            this.cmbxInterval.Size = new System.Drawing.Size(80, 24);
            this.cmbxInterval.TabIndex = 7;
            // 
            // cbxInterval
            // 
            this.cbxInterval.AutoSize = true;
            this.cbxInterval.Location = new System.Drawing.Point(556, 89);
            this.cbxInterval.Margin = new System.Windows.Forms.Padding(4);
            this.cbxInterval.Name = "cbxInterval";
            this.cbxInterval.Size = new System.Drawing.Size(112, 21);
            this.cbxInterval.TabIndex = 6;
            this.cbxInterval.Text = "Interval (sec)";
            this.cbxInterval.UseVisualStyleBackColor = true;
            this.cbxInterval.CheckedChanged += new System.EventHandler(this.cbxInterval_CheckedChanged);
            // 
            // cbxUsePCAPTime
            // 
            this.cbxUsePCAPTime.AutoSize = true;
            this.cbxUsePCAPTime.Checked = true;
            this.cbxUsePCAPTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxUsePCAPTime.Location = new System.Drawing.Point(381, 90);
            this.cbxUsePCAPTime.Margin = new System.Windows.Forms.Padding(4);
            this.cbxUsePCAPTime.Name = "cbxUsePCAPTime";
            this.cbxUsePCAPTime.Size = new System.Drawing.Size(163, 21);
            this.cbxUsePCAPTime.TabIndex = 5;
            this.cbxUsePCAPTime.Text = "Play using PCap time";
            this.cbxUsePCAPTime.UseVisualStyleBackColor = true;
            this.cbxUsePCAPTime.CheckedChanged += new System.EventHandler(this.cbxUsePCAPTime_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 91);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Destination:";
            // 
            // tbPCap
            // 
            this.tbPCap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPCap.Location = new System.Drawing.Point(93, 20);
            this.tbPCap.Margin = new System.Windows.Forms.Padding(4);
            this.tbPCap.Name = "tbPCap";
            this.tbPCap.Size = new System.Drawing.Size(659, 56);
            this.tbPCap.TabIndex = 3;
            this.tbPCap.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbPCap.Scroll += new System.EventHandler(this.tbPCap_Scroll);
            // 
            // tbxDestination
            // 
            this.tbxDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDestination.Location = new System.Drawing.Point(116, 87);
            this.tbxDestination.Margin = new System.Windows.Forms.Padding(4);
            this.tbxDestination.Name = "tbxDestination";
            this.tbxDestination.Size = new System.Drawing.Size(132, 27);
            this.tbxDestination.TabIndex = 2;
            this.tbxDestination.Text = "192.168.43.31";
            // 
            // btnPlay
            // 
            this.btnPlay.ImageIndex = 0;
            this.btnPlay.ImageList = this.imageList1;
            this.btnPlay.Location = new System.Drawing.Point(25, 23);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(4);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(60, 55);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "play_32.png");
            this.imageList1.Images.SetKeyName(1, "stop32.png");
            // 
            // lbxPCAP
            // 
            this.lbxPCAP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxPCAP.BackColor = System.Drawing.Color.Black;
            this.lbxPCAP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbxPCAP.ContextMenuStrip = this.contextMenuStrip1;
            this.lbxPCAP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxPCAP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbxPCAP.FormattingEnabled = true;
            this.lbxPCAP.ItemHeight = 17;
            this.lbxPCAP.Location = new System.Drawing.Point(16, 289);
            this.lbxPCAP.Margin = new System.Windows.Forms.Padding(4);
            this.lbxPCAP.Name = "lbxPCAP";
            this.lbxPCAP.ScrollAlwaysVisible = true;
            this.lbxPCAP.Size = new System.Drawing.Size(759, 546);
            this.lbxPCAP.TabIndex = 2;
            this.toolTip1.SetToolTip(this.lbxPCAP, "Doubleclick to send packet");
            this.lbxPCAP.Click += new System.EventHandler(this.lbxPCAP_Click);
            this.lbxPCAP.DoubleClick += new System.EventHandler(this.lbxPCAP_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendThisPacketToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(187, 28);
            // 
            // sendThisPacketToolStripMenuItem
            // 
            this.sendThisPacketToolStripMenuItem.Name = "sendThisPacketToolStripMenuItem";
            this.sendThisPacketToolStripMenuItem.Size = new System.Drawing.Size(186, 24);
            this.sendThisPacketToolStripMenuItem.Text = "Send this packet";
            this.sendThisPacketToolStripMenuItem.Click += new System.EventHandler(this.sendThisPacketToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 866);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(792, 26);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.LightGray;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 21);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(167, 20);
            // 
            // FrmMainStub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(792, 892);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lbxPCAP);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMainStub";
            this.Text = "PCap tester";
            this.Load += new System.EventHandler(this.FrmMainStub_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPCap)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelectLogDir;
        private System.Windows.Forms.TextBox tbxLog4NetDirectory;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lbxPCAP;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox tbxDestination;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbPCap;
        private System.Windows.Forms.CheckBox cbxUsePCAPTime;
        private System.Windows.Forms.ComboBox cmbxInterval;
        private System.Windows.Forms.CheckBox cbxInterval;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sendThisPacketToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxPort;
    }
}

