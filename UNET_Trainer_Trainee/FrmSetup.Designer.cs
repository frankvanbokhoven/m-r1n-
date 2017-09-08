namespace UNET_Trainer_Trainee
{
    partial class FrmSetup
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.rbDark = new System.Windows.Forms.RadioButton();
            this.btnBlue = new System.Windows.Forms.Button();
            this.btnLight = new System.Windows.Forms.Button();
            this.btnDark = new System.Windows.Forms.Button();
            this.btnApplyColors = new System.Windows.Forms.Button();
            this.lblTestFont = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ddlFont = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlColorExample = new System.Windows.Forms.Panel();
            this.ddlColorButton = new System.Windows.Forms.ComboBox();
            this.btnMainPage = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLogDirectory = new System.Windows.Forms.TextBox();
            this.btnSelectLogDir = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.rbDark);
            this.groupBox1.Controls.Add(this.btnBlue);
            this.groupBox1.Controls.Add(this.btnLight);
            this.groupBox1.Controls.Add(this.btnDark);
            this.groupBox1.Location = new System.Drawing.Point(68, 391);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.groupBox1.Size = new System.Drawing.Size(600, 261);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Color theme";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(466, 209);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(14, 13);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(276, 209);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(14, 13);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // rbDark
            // 
            this.rbDark.AutoSize = true;
            this.rbDark.Checked = true;
            this.rbDark.Location = new System.Drawing.Point(96, 209);
            this.rbDark.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.rbDark.Name = "rbDark";
            this.rbDark.Size = new System.Drawing.Size(14, 13);
            this.rbDark.TabIndex = 3;
            this.rbDark.TabStop = true;
            this.rbDark.UseVisualStyleBackColor = true;
            // 
            // btnBlue
            // 
            this.btnBlue.BackColor = System.Drawing.Color.SkyBlue;
            this.btnBlue.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBlue.ForeColor = System.Drawing.Color.White;
            this.btnBlue.Location = new System.Drawing.Point(396, 34);
            this.btnBlue.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnBlue.Name = "btnBlue";
            this.btnBlue.Size = new System.Drawing.Size(180, 164);
            this.btnBlue.TabIndex = 2;
            this.btnBlue.Text = "Blue";
            this.btnBlue.UseVisualStyleBackColor = false;
            // 
            // btnLight
            // 
            this.btnLight.BackColor = System.Drawing.Color.White;
            this.btnLight.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLight.ForeColor = System.Drawing.Color.Black;
            this.btnLight.Location = new System.Drawing.Point(204, 34);
            this.btnLight.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnLight.Name = "btnLight";
            this.btnLight.Size = new System.Drawing.Size(180, 164);
            this.btnLight.TabIndex = 1;
            this.btnLight.Text = "Light";
            this.btnLight.UseVisualStyleBackColor = false;
            // 
            // btnDark
            // 
            this.btnDark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDark.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDark.ForeColor = System.Drawing.Color.White;
            this.btnDark.Location = new System.Drawing.Point(12, 34);
            this.btnDark.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnDark.Name = "btnDark";
            this.btnDark.Size = new System.Drawing.Size(180, 164);
            this.btnDark.TabIndex = 0;
            this.btnDark.Text = "Dark";
            this.btnDark.UseVisualStyleBackColor = false;
            // 
            // btnApplyColors
            // 
            this.btnApplyColors.BackColor = System.Drawing.Color.Aqua;
            this.btnApplyColors.Location = new System.Drawing.Point(660, 733);
            this.btnApplyColors.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnApplyColors.Name = "btnApplyColors";
            this.btnApplyColors.Size = new System.Drawing.Size(228, 78);
            this.btnApplyColors.TabIndex = 32;
            this.btnApplyColors.Text = "Apply changes";
            this.btnApplyColors.UseVisualStyleBackColor = false;
            // 
            // lblTestFont
            // 
            this.lblTestFont.AutoSize = true;
            this.lblTestFont.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestFont.Location = new System.Drawing.Point(538, 222);
            this.lblTestFont.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTestFont.Name = "lblTestFont";
            this.lblTestFont.Size = new System.Drawing.Size(90, 22);
            this.lblTestFont.TabIndex = 31;
            this.lblTestFont.Text = "Test font";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 180);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 25);
            this.label2.TabIndex = 30;
            this.label2.Text = "Font";
            // 
            // ddlFont
            // 
            this.ddlFont.FormattingEnabled = true;
            this.ddlFont.Location = new System.Drawing.Point(69, 214);
            this.ddlFont.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddlFont.Name = "ddlFont";
            this.ddlFont.Size = new System.Drawing.Size(442, 33);
            this.ddlFont.TabIndex = 29;
            this.ddlFont.SelectedIndexChanged += new System.EventHandler(this.ddlFont_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 25);
            this.label1.TabIndex = 28;
            this.label1.Text = "Color of buttons";
            // 
            // pnlColorExample
            // 
            this.pnlColorExample.Location = new System.Drawing.Point(534, 98);
            this.pnlColorExample.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlColorExample.Name = "pnlColorExample";
            this.pnlColorExample.Size = new System.Drawing.Size(52, 39);
            this.pnlColorExample.TabIndex = 27;
            // 
            // ddlColorButton
            // 
            this.ddlColorButton.FormattingEnabled = true;
            this.ddlColorButton.Location = new System.Drawing.Point(69, 98);
            this.ddlColorButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddlColorButton.Name = "ddlColorButton";
            this.ddlColorButton.Size = new System.Drawing.Size(442, 33);
            this.ddlColorButton.TabIndex = 26;
            this.ddlColorButton.SelectedIndexChanged += new System.EventHandler(this.ddlColorButton_SelectedIndexChanged);
            // 
            // btnMainPage
            // 
            this.btnMainPage.BackColor = System.Drawing.Color.Aqua;
            this.btnMainPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMainPage.Location = new System.Drawing.Point(68, 733);
            this.btnMainPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnMainPage.Name = "btnMainPage";
            this.btnMainPage.Size = new System.Drawing.Size(228, 83);
            this.btnMainPage.TabIndex = 34;
            this.btnMainPage.Text = "Main page";
            this.btnMainPage.UseVisualStyleBackColor = false;
            this.btnMainPage.Click += new System.EventHandler(this.btnMainPage_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 286);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 25);
            this.label3.TabIndex = 35;
            this.label3.Text = "Log Directory";
            // 
            // txtLogDirectory
            // 
            this.txtLogDirectory.BackColor = System.Drawing.SystemColors.ControlDark;
            this.txtLogDirectory.Location = new System.Drawing.Point(69, 317);
            this.txtLogDirectory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLogDirectory.Name = "txtLogDirectory";
            this.txtLogDirectory.ReadOnly = true;
            this.txtLogDirectory.Size = new System.Drawing.Size(656, 31);
            this.txtLogDirectory.TabIndex = 36;
            this.txtLogDirectory.Text = "c:\\Marine\\GitSources\\Log\\UNET_Trainer_Trainee.log";
            // 
            // btnSelectLogDir
            // 
            this.btnSelectLogDir.Image = global::UNET_Trainer_Trainee.Properties.Resources.open_icon;
            this.btnSelectLogDir.Location = new System.Drawing.Point(747, 307);
            this.btnSelectLogDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelectLogDir.Name = "btnSelectLogDir";
            this.btnSelectLogDir.Size = new System.Drawing.Size(48, 50);
            this.btnSelectLogDir.TabIndex = 37;
            this.btnSelectLogDir.UseVisualStyleBackColor = true;
            this.btnSelectLogDir.Click += new System.EventHandler(this.btnSelectLogDir_Click);
            // 
            // FrmSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1494, 1088);
            this.Controls.Add(this.btnSelectLogDir);
            this.Controls.Add(this.txtLogDirectory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnApplyColors);
            this.Controls.Add(this.lblTestFont);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ddlFont);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlColorExample);
            this.Controls.Add(this.ddlColorButton);
            this.Controls.Add(this.btnMainPage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FrmSetup";
            this.Load += new System.EventHandler(this.FrmSetup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton rbDark;
        private System.Windows.Forms.Button btnBlue;
        private System.Windows.Forms.Button btnLight;
        private System.Windows.Forms.Button btnDark;
        private System.Windows.Forms.Button btnApplyColors;
        private System.Windows.Forms.Label lblTestFont;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ddlFont;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlColorExample;
        private System.Windows.Forms.ComboBox ddlColorButton;
        private System.Windows.Forms.Button btnMainPage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLogDirectory;
        private System.Windows.Forms.Button btnSelectLogDir;
    }
}