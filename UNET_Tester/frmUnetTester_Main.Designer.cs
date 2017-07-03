namespace UNET_Tester
{
    partial class frmUNETTester_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUNETTester_Main));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grbxNoiseLevel = new System.Windows.Forms.GroupBox();
            this.lbxNoiseLevel = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxRadios = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxRole = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxTrainee = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxExercise = new System.Windows.Forms.ComboBox();
            this.listBoxGetmethods = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnClear = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxInstructor = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.grbxNoiseLevel.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbxInstructor);
            this.groupBox1.Controls.Add(this.grbxNoiseLevel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbxRadios);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxRole);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbxTrainee);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxExercise);
            this.groupBox1.Location = new System.Drawing.Point(16, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(680, 251);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // grbxNoiseLevel
            // 
            this.grbxNoiseLevel.Controls.Add(this.lbxNoiseLevel);
            this.grbxNoiseLevel.Dock = System.Windows.Forms.DockStyle.Right;
            this.grbxNoiseLevel.Location = new System.Drawing.Point(477, 17);
            this.grbxNoiseLevel.Name = "grbxNoiseLevel";
            this.grbxNoiseLevel.Size = new System.Drawing.Size(200, 232);
            this.grbxNoiseLevel.TabIndex = 8;
            this.grbxNoiseLevel.TabStop = false;
            this.grbxNoiseLevel.Text = "Noise level";
            // 
            // lbxNoiseLevel
            // 
            this.lbxNoiseLevel.BackColor = System.Drawing.Color.DimGray;
            this.lbxNoiseLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxNoiseLevel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxNoiseLevel.ForeColor = System.Drawing.Color.White;
            this.lbxNoiseLevel.FormattingEnabled = true;
            this.lbxNoiseLevel.ItemHeight = 20;
            this.lbxNoiseLevel.Items.AddRange(new object[] {
            "test",
            "test",
            "test"});
            this.lbxNoiseLevel.Location = new System.Drawing.Point(3, 18);
            this.lbxNoiseLevel.Name = "lbxNoiseLevel";
            this.lbxNoiseLevel.Size = new System.Drawing.Size(194, 211);
            this.lbxNoiseLevel.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Radio";
            // 
            // cbxRadios
            // 
            this.cbxRadios.FormattingEnabled = true;
            this.cbxRadios.Items.AddRange(new object[] {
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
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cbxRadios.Location = new System.Drawing.Point(116, 181);
            this.cbxRadios.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxRadios.Name = "cbxRadios";
            this.cbxRadios.Size = new System.Drawing.Size(120, 24);
            this.cbxRadios.TabIndex = 6;
            this.cbxRadios.Text = "4";
            this.cbxRadios.SelectedValueChanged += new System.EventHandler(this.cbxRadios_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Role";
            // 
            // cbxRole
            // 
            this.cbxRole.FormattingEnabled = true;
            this.cbxRole.Items.AddRange(new object[] {
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
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cbxRole.Location = new System.Drawing.Point(116, 138);
            this.cbxRole.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxRole.Name = "cbxRole";
            this.cbxRole.Size = new System.Drawing.Size(120, 24);
            this.cbxRole.TabIndex = 4;
            this.cbxRole.Text = "4";
            this.cbxRole.SelectedIndexChanged += new System.EventHandler(this.cbxRole_SelectedIndexChanged);
            this.cbxRole.SelectedValueChanged += new System.EventHandler(this.cbxRole_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Trainees";
            // 
            // cbxTrainee
            // 
            this.cbxTrainee.FormattingEnabled = true;
            this.cbxTrainee.Items.AddRange(new object[] {
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
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.cbxTrainee.Location = new System.Drawing.Point(116, 95);
            this.cbxTrainee.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxTrainee.Name = "cbxTrainee";
            this.cbxTrainee.Size = new System.Drawing.Size(120, 24);
            this.cbxTrainee.TabIndex = 2;
            this.cbxTrainee.Text = "4";
            this.cbxTrainee.SelectedValueChanged += new System.EventHandler(this.cbxTrainee_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Exercise";
            // 
            // cbxExercise
            // 
            this.cbxExercise.FormattingEnabled = true;
            this.cbxExercise.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cbxExercise.Location = new System.Drawing.Point(116, 50);
            this.cbxExercise.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxExercise.Name = "cbxExercise";
            this.cbxExercise.Size = new System.Drawing.Size(120, 24);
            this.cbxExercise.TabIndex = 0;
            this.cbxExercise.Text = "4";
            this.cbxExercise.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // listBoxGetmethods
            // 
            this.listBoxGetmethods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxGetmethods.BackColor = System.Drawing.SystemColors.HotTrack;
            this.listBoxGetmethods.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxGetmethods.ForeColor = System.Drawing.SystemColors.Window;
            this.listBoxGetmethods.FormattingEnabled = true;
            this.listBoxGetmethods.ItemHeight = 18;
            this.listBoxGetmethods.Location = new System.Drawing.Point(16, 271);
            this.listBoxGetmethods.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxGetmethods.Name = "listBoxGetmethods";
            this.listBoxGetmethods.Size = new System.Drawing.Size(675, 616);
            this.listBoxGetmethods.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.SystemColors.Control;
            this.btnClear.Location = new System.Drawing.Point(16, 892);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 31);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuit.BackColor = System.Drawing.Color.Red;
            this.btnQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnQuit.Location = new System.Drawing.Point(536, 892);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 31);
            this.btnQuit.TabIndex = 3;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = false;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(270, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Instructor";
            // 
            // cbxInstructor
            // 
            this.cbxInstructor.FormattingEnabled = true;
            this.cbxInstructor.Items.AddRange(new object[] {
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
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cbxInstructor.Location = new System.Drawing.Point(337, 50);
            this.cbxInstructor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxInstructor.Name = "cbxInstructor";
            this.cbxInstructor.Size = new System.Drawing.Size(120, 24);
            this.cbxInstructor.TabIndex = 9;
            this.cbxInstructor.Text = "4";
            this.cbxInstructor.SelectedValueChanged += new System.EventHandler(this.cbxInstructor_SelectedValueChanged);
            // 
            // frmUNETTester_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(708, 928);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.listBoxGetmethods);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmUNETTester_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UNET Tester";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUNETTester_Main_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grbxNoiseLevel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxExercise;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxRadios;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxRole;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxTrainee;
        private System.Windows.Forms.ListBox listBoxGetmethods;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.GroupBox grbxNoiseLevel;
        private System.Windows.Forms.ListBox lbxNoiseLevel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxInstructor;
    }
}

