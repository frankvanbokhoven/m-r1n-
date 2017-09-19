namespace UNET_Trainer
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
            this.lblTestFont = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ddlFont = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlColorExample = new System.Windows.Forms.Panel();
            this.ddlColorButton = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbBlue = new System.Windows.Forms.RadioButton();
            this.rbLight = new System.Windows.Forms.RadioButton();
            this.rbDark = new System.Windows.Forms.RadioButton();
            this.btnBlue = new System.Windows.Forms.Button();
            this.btnLight = new System.Windows.Forms.Button();
            this.btnDark = new System.Windows.Forms.Button();
            this.btnMainPage = new System.Windows.Forms.Button();
            this.btnSelectLogDir = new System.Windows.Forms.Button();
            this.txtLogDirectory = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxAccount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTestFont
            // 
            this.lblTestFont.AutoSize = true;
            this.lblTestFont.Location = new System.Drawing.Point(381, 150);
            this.lblTestFont.Name = "lblTestFont";
            this.lblTestFont.Size = new System.Drawing.Size(81, 20);
            this.lblTestFont.TabIndex = 22;
            this.lblTestFont.Text = "Test font";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 20);
            this.label2.TabIndex = 21;
            this.label2.Text = "Font";
            // 
            // ddlFont
            // 
            this.ddlFont.FormattingEnabled = true;
            this.ddlFont.Location = new System.Drawing.Point(68, 145);
            this.ddlFont.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ddlFont.Name = "ddlFont";
            this.ddlFont.Size = new System.Drawing.Size(296, 26);
            this.ddlFont.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "Color of buttons";
            // 
            // pnlColorExample
            // 
            this.pnlColorExample.Location = new System.Drawing.Point(378, 71);
            this.pnlColorExample.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlColorExample.Name = "pnlColorExample";
            this.pnlColorExample.Size = new System.Drawing.Size(35, 25);
            this.pnlColorExample.TabIndex = 18;
            // 
            // ddlColorButton
            // 
            this.ddlColorButton.FormattingEnabled = true;
            this.ddlColorButton.Location = new System.Drawing.Point(68, 71);
            this.ddlColorButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ddlColorButton.Name = "ddlColorButton";
            this.ddlColorButton.Size = new System.Drawing.Size(296, 26);
            this.ddlColorButton.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbBlue);
            this.groupBox1.Controls.Add(this.rbLight);
            this.groupBox1.Controls.Add(this.rbDark);
            this.groupBox1.Controls.Add(this.btnBlue);
            this.groupBox1.Controls.Add(this.btnLight);
            this.groupBox1.Controls.Add(this.btnDark);
            this.groupBox1.Location = new System.Drawing.Point(67, 360);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(400, 167);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Color theme";
            // 
            // rbBlue
            // 
            this.rbBlue.AutoSize = true;
            this.rbBlue.Location = new System.Drawing.Point(311, 134);
            this.rbBlue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rbBlue.Name = "rbBlue";
            this.rbBlue.Size = new System.Drawing.Size(17, 16);
            this.rbBlue.TabIndex = 5;
            this.rbBlue.UseVisualStyleBackColor = true;
            // 
            // rbLight
            // 
            this.rbLight.AutoSize = true;
            this.rbLight.Location = new System.Drawing.Point(184, 134);
            this.rbLight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rbLight.Name = "rbLight";
            this.rbLight.Size = new System.Drawing.Size(17, 16);
            this.rbLight.TabIndex = 4;
            this.rbLight.UseVisualStyleBackColor = true;
            // 
            // rbDark
            // 
            this.rbDark.AutoSize = true;
            this.rbDark.Checked = true;
            this.rbDark.Location = new System.Drawing.Point(64, 134);
            this.rbDark.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rbDark.Name = "rbDark";
            this.rbDark.Size = new System.Drawing.Size(17, 16);
            this.rbDark.TabIndex = 3;
            this.rbDark.TabStop = true;
            this.rbDark.UseVisualStyleBackColor = true;
            // 
            // btnBlue
            // 
            this.btnBlue.BackColor = System.Drawing.Color.SkyBlue;
            this.btnBlue.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBlue.ForeColor = System.Drawing.Color.White;
            this.btnBlue.Location = new System.Drawing.Point(264, 22);
            this.btnBlue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnBlue.Name = "btnBlue";
            this.btnBlue.Size = new System.Drawing.Size(120, 105);
            this.btnBlue.TabIndex = 2;
            this.btnBlue.Text = "Blue";
            this.btnBlue.UseVisualStyleBackColor = false;
            // 
            // btnLight
            // 
            this.btnLight.BackColor = System.Drawing.Color.White;
            this.btnLight.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLight.ForeColor = System.Drawing.Color.Black;
            this.btnLight.Location = new System.Drawing.Point(136, 22);
            this.btnLight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnLight.Name = "btnLight";
            this.btnLight.Size = new System.Drawing.Size(120, 105);
            this.btnLight.TabIndex = 1;
            this.btnLight.Text = "Light";
            this.btnLight.UseVisualStyleBackColor = false;
            // 
            // btnDark
            // 
            this.btnDark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDark.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDark.ForeColor = System.Drawing.Color.White;
            this.btnDark.Location = new System.Drawing.Point(8, 22);
            this.btnDark.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDark.Name = "btnDark";
            this.btnDark.Size = new System.Drawing.Size(120, 105);
            this.btnDark.TabIndex = 0;
            this.btnDark.Text = "Dark";
            this.btnDark.UseVisualStyleBackColor = false;
            // 
            // btnMainPage
            // 
            this.btnMainPage.BackColor = System.Drawing.Color.Aqua;
            this.btnMainPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMainPage.Location = new System.Drawing.Point(68, 563);
            this.btnMainPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMainPage.Name = "btnMainPage";
            this.btnMainPage.Size = new System.Drawing.Size(152, 53);
            this.btnMainPage.TabIndex = 25;
            this.btnMainPage.Text = "Main page";
            this.btnMainPage.UseVisualStyleBackColor = false;
            this.btnMainPage.Click += new System.EventHandler(this.btnMainPage_Click);
            // 
            // btnSelectLogDir
            // 
            this.btnSelectLogDir.Image = global::UNET_Trainer.Properties.Resources.open_icon;
            this.btnSelectLogDir.Location = new System.Drawing.Point(603, 211);
            this.btnSelectLogDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelectLogDir.Name = "btnSelectLogDir";
            this.btnSelectLogDir.Size = new System.Drawing.Size(50, 50);
            this.btnSelectLogDir.TabIndex = 40;
            this.btnSelectLogDir.UseVisualStyleBackColor = true;
            this.btnSelectLogDir.Click += new System.EventHandler(this.btnSelectLogDir_Click);
            // 
            // txtLogDirectory
            // 
            this.txtLogDirectory.BackColor = System.Drawing.SystemColors.ControlDark;
            this.txtLogDirectory.Location = new System.Drawing.Point(68, 226);
            this.txtLogDirectory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLogDirectory.Name = "txtLogDirectory";
            this.txtLogDirectory.ReadOnly = true;
            this.txtLogDirectory.Size = new System.Drawing.Size(502, 26);
            this.txtLogDirectory.TabIndex = 39;
            this.txtLogDirectory.Text = "c:\\Marine\\GitSources\\Log\\UNET_Trainer_Trainee.log";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 195);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 20);
            this.label3.TabIndex = 38;
            this.label3.Text = "Log Directory";
            // 
            // tbxAccount
            // 
            this.tbxAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAccount.Location = new System.Drawing.Point(67, 298);
            this.tbxAccount.Name = "tbxAccount";
            this.tbxAccount.Size = new System.Drawing.Size(296, 24);
            this.tbxAccount.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 278);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 20);
            this.label4.TabIndex = 41;
            this.label4.Text = "Account";
            // 
            // FrmSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 747);
            this.Controls.Add(this.tbxAccount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSelectLogDir);
            this.Controls.Add(this.txtLogDirectory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnMainPage);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTestFont);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ddlFont);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlColorExample);
            this.Controls.Add(this.ddlColorButton);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmSetup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Setup";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSetup_FormClosing);
            this.Load += new System.EventHandler(this.FrmSetup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTestFont;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ddlFont;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlColorExample;
        private System.Windows.Forms.ComboBox ddlColorButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbBlue;
        private System.Windows.Forms.RadioButton rbLight;
        private System.Windows.Forms.RadioButton rbDark;
        private System.Windows.Forms.Button btnBlue;
        private System.Windows.Forms.Button btnLight;
        private System.Windows.Forms.Button btnDark;
        private System.Windows.Forms.Button btnMainPage;
        private System.Windows.Forms.Button btnSelectLogDir;
        private System.Windows.Forms.TextBox txtLogDirectory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxAccount;
        private System.Windows.Forms.Label label4;
    }
}