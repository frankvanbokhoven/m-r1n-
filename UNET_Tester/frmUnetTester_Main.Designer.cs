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
            this.btnRefreshInstructors = new System.Windows.Forms.Button();
            this.tbxInstructorIDs = new System.Windows.Forms.TextBox();
            this.btnRefreshTrainees = new System.Windows.Forms.Button();
            this.tbxTraineeIDs = new System.Windows.Forms.TextBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSpecification = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.grbxNoiseLevel = new System.Windows.Forms.GroupBox();
            this.lbxNoiseLevel = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxRadios = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxRole = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxExercise = new System.Windows.Forms.ComboBox();
            this.listBoxGetmethods = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cbxKeepAlive = new System.Windows.Forms.CheckBox();
            this.cbxAssignRoles = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.grbxNoiseLevel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxAssignRoles);
            this.groupBox1.Controls.Add(this.btnRefreshInstructors);
            this.groupBox1.Controls.Add(this.tbxInstructorIDs);
            this.groupBox1.Controls.Add(this.btnRefreshTrainees);
            this.groupBox1.Controls.Add(this.tbxTraineeIDs);
            this.groupBox1.Controls.Add(this.buttonRefresh);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtSpecification);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.grbxNoiseLevel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbxRadios);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxRole);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxExercise);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(530, 219);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnRefreshInstructors
            // 
            this.btnRefreshInstructors.Image = global::UNET_Tester.Properties.Resources.Button_Refresh_icon;
            this.btnRefreshInstructors.Location = new System.Drawing.Point(260, 149);
            this.btnRefreshInstructors.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRefreshInstructors.Name = "btnRefreshInstructors";
            this.btnRefreshInstructors.Size = new System.Drawing.Size(30, 32);
            this.btnRefreshInstructors.TabIndex = 19;
            this.toolTip1.SetToolTip(this.btnRefreshInstructors, "Refresh trainees");
            this.btnRefreshInstructors.UseVisualStyleBackColor = true;
            this.btnRefreshInstructors.Click += new System.EventHandler(this.btnRefreshInstructors_Click);
            // 
            // tbxInstructorIDs
            // 
            this.tbxInstructorIDs.Location = new System.Drawing.Point(78, 153);
            this.tbxInstructorIDs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxInstructorIDs.Name = "tbxInstructorIDs";
            this.tbxInstructorIDs.Size = new System.Drawing.Size(169, 20);
            this.tbxInstructorIDs.TabIndex = 18;
            this.tbxInstructorIDs.Text = "1016";
            this.toolTip1.SetToolTip(this.tbxInstructorIDs, "Vul hier de id\'s in van de trainees en druk dan op refesh trainees");
            // 
            // btnRefreshTrainees
            // 
            this.btnRefreshTrainees.Image = global::UNET_Tester.Properties.Resources.Button_Refresh_icon;
            this.btnRefreshTrainees.Location = new System.Drawing.Point(260, 65);
            this.btnRefreshTrainees.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRefreshTrainees.Name = "btnRefreshTrainees";
            this.btnRefreshTrainees.Size = new System.Drawing.Size(30, 32);
            this.btnRefreshTrainees.TabIndex = 17;
            this.toolTip1.SetToolTip(this.btnRefreshTrainees, "Refresh trainees");
            this.btnRefreshTrainees.UseVisualStyleBackColor = true;
            this.btnRefreshTrainees.Click += new System.EventHandler(this.btnRefreshTrainees_Click);
            // 
            // tbxTraineeIDs
            // 
            this.tbxTraineeIDs.Location = new System.Drawing.Point(78, 69);
            this.tbxTraineeIDs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxTraineeIDs.Name = "tbxTraineeIDs";
            this.tbxTraineeIDs.Size = new System.Drawing.Size(169, 20);
            this.tbxTraineeIDs.TabIndex = 16;
            this.tbxTraineeIDs.Text = "1000,1001,1002,1003";
            this.toolTip1.SetToolTip(this.tbxTraineeIDs, "Vul hier de id\'s in van de trainees en druk dan op refesh trainees");
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.BackColor = System.Drawing.Color.LawnGreen;
            this.buttonRefresh.Location = new System.Drawing.Point(244, 188);
            this.buttonRefresh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(56, 27);
            this.buttonRefresh.TabIndex = 15;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(212, 24);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(214, 41);
            this.txtName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(87, 20);
            this.txtName.TabIndex = 13;
            this.txtName.Text = "Exc_name_";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(122, 24);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Specification";
            // 
            // txtSpecification
            // 
            this.txtSpecification.Location = new System.Drawing.Point(124, 41);
            this.txtSpecification.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSpecification.Name = "txtSpecification";
            this.txtSpecification.Size = new System.Drawing.Size(87, 20);
            this.txtSpecification.TabIndex = 11;
            this.txtSpecification.Text = "specifi_";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 153);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Instructor";
            // 
            // grbxNoiseLevel
            // 
            this.grbxNoiseLevel.Controls.Add(this.lbxNoiseLevel);
            this.grbxNoiseLevel.Dock = System.Windows.Forms.DockStyle.Right;
            this.grbxNoiseLevel.Location = new System.Drawing.Point(316, 15);
            this.grbxNoiseLevel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grbxNoiseLevel.Name = "grbxNoiseLevel";
            this.grbxNoiseLevel.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grbxNoiseLevel.Size = new System.Drawing.Size(212, 202);
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
            this.lbxNoiseLevel.ItemHeight = 16;
            this.lbxNoiseLevel.Items.AddRange(new object[] {
            "test",
            "test",
            "test"});
            this.lbxNoiseLevel.Location = new System.Drawing.Point(2, 15);
            this.lbxNoiseLevel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lbxNoiseLevel.Name = "lbxNoiseLevel";
            this.lbxNoiseLevel.Size = new System.Drawing.Size(208, 185);
            this.lbxNoiseLevel.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 130);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
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
            this.cbxRadios.Location = new System.Drawing.Point(78, 128);
            this.cbxRadios.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbxRadios.Name = "cbxRadios";
            this.cbxRadios.Size = new System.Drawing.Size(42, 21);
            this.cbxRadios.TabIndex = 6;
            this.cbxRadios.Text = "4";
            this.cbxRadios.SelectedValueChanged += new System.EventHandler(this.cbxRadios_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 107);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Role / PtP";
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
            this.cbxRole.Location = new System.Drawing.Point(78, 105);
            this.cbxRole.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbxRole.Name = "cbxRole";
            this.cbxRole.Size = new System.Drawing.Size(42, 21);
            this.cbxRole.TabIndex = 4;
            this.cbxRole.Text = "4";
            this.cbxRole.SelectedValueChanged += new System.EventHandler(this.cbxRole_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 73);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Trainees";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 43);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
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
            this.cbxExercise.Location = new System.Drawing.Point(78, 41);
            this.cbxExercise.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbxExercise.Name = "cbxExercise";
            this.cbxExercise.Size = new System.Drawing.Size(42, 21);
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
            this.listBoxGetmethods.ItemHeight = 15;
            this.listBoxGetmethods.Location = new System.Drawing.Point(12, 266);
            this.listBoxGetmethods.Name = "listBoxGetmethods";
            this.listBoxGetmethods.Size = new System.Drawing.Size(531, 349);
            this.listBoxGetmethods.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 250;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnQuit);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.cbxKeepAlive);
            this.panel1.Location = new System.Drawing.Point(12, 233);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(529, 30);
            this.panel1.TabIndex = 4;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.SystemColors.Control;
            this.btnClear.Location = new System.Drawing.Point(397, 2);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(56, 23);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear list";
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuit.BackColor = System.Drawing.Color.Red;
            this.btnQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuit.ForeColor = System.Drawing.SystemColors.Control;
            this.btnQuit.Location = new System.Drawing.Point(461, 2);
            this.btnQuit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(56, 23);
            this.btnQuit.TabIndex = 4;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "60",
            "120",
            "180",
            "300",
            "600"});
            this.comboBox1.Location = new System.Drawing.Point(158, 5);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(42, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.Text = "180";
            // 
            // cbxKeepAlive
            // 
            this.cbxKeepAlive.AutoSize = true;
            this.cbxKeepAlive.Checked = true;
            this.cbxKeepAlive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxKeepAlive.Location = new System.Drawing.Point(10, 6);
            this.cbxKeepAlive.Margin = new System.Windows.Forms.Padding(2);
            this.cbxKeepAlive.Name = "cbxKeepAlive";
            this.cbxKeepAlive.Size = new System.Drawing.Size(154, 17);
            this.cbxKeepAlive.TabIndex = 0;
            this.cbxKeepAlive.Text = "Keep alive (every seconds)";
            this.cbxKeepAlive.UseVisualStyleBackColor = true;
            this.cbxKeepAlive.CheckedChanged += new System.EventHandler(this.cbxKeepAlive_CheckedChanged);
            // 
            // cbxAssignRoles
            // 
            this.cbxAssignRoles.AutoSize = true;
            this.cbxAssignRoles.Checked = true;
            this.cbxAssignRoles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxAssignRoles.Location = new System.Drawing.Point(78, 178);
            this.cbxAssignRoles.Name = "cbxAssignRoles";
            this.cbxAssignRoles.Size = new System.Drawing.Size(159, 17);
            this.cbxAssignRoles.TabIndex = 20;
            this.cbxAssignRoles.Text = "Assign roles to first instructor";
            this.cbxAssignRoles.UseVisualStyleBackColor = true;
            // 
            // frmUNETTester_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(555, 641);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listBoxGetmethods);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmUNETTester_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UNET Tester";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUNETTester_Main_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grbxNoiseLevel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.ListBox listBoxGetmethods;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox grbxNoiseLevel;
        private System.Windows.Forms.ListBox lbxNoiseLevel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSpecification;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.TextBox tbxTraineeIDs;
        private System.Windows.Forms.Button btnRefreshTrainees;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnRefreshInstructors;
        private System.Windows.Forms.TextBox tbxInstructorIDs;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox cbxKeepAlive;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.CheckBox cbxAssignRoles;
    }
}

