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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTestFont = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ddlFont = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlColorExample = new System.Windows.Forms.Panel();
            this.ddlColorButton = new System.Windows.Forms.ComboBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTestFont);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ddlFont);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pnlColorExample);
            this.groupBox1.Controls.Add(this.ddlColorButton);
            this.groupBox1.Controls.Add(this.btnApply);
            this.groupBox1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(43, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(691, 452);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Colors for Trainer";
            // 
            // lblTestFont
            // 
            this.lblTestFont.AutoSize = true;
            this.lblTestFont.Location = new System.Drawing.Point(351, 165);
            this.lblTestFont.Name = "lblTestFont";
            this.lblTestFont.Size = new System.Drawing.Size(81, 20);
            this.lblTestFont.TabIndex = 16;
            this.lblTestFont.Text = "Test font";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Font";
            // 
            // ddlFont
            // 
            this.ddlFont.FormattingEnabled = true;
            this.ddlFont.Location = new System.Drawing.Point(37, 160);
            this.ddlFont.Name = "ddlFont";
            this.ddlFont.Size = new System.Drawing.Size(296, 28);
            this.ddlFont.TabIndex = 13;
            this.ddlFont.SelectedIndexChanged += new System.EventHandler(this.ddlFont_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Color of buttons";
            // 
            // pnlColorExample
            // 
            this.pnlColorExample.Location = new System.Drawing.Point(348, 81);
            this.pnlColorExample.Name = "pnlColorExample";
            this.pnlColorExample.Size = new System.Drawing.Size(34, 27);
            this.pnlColorExample.TabIndex = 11;
            // 
            // ddlColorButton
            // 
            this.ddlColorButton.FormattingEnabled = true;
            this.ddlColorButton.Location = new System.Drawing.Point(37, 81);
            this.ddlColorButton.Name = "ddlColorButton";
            this.ddlColorButton.Size = new System.Drawing.Size(296, 28);
            this.ddlColorButton.TabIndex = 10;
            this.ddlColorButton.SelectedIndexChanged += new System.EventHandler(this.ddlColorButton_SelectedIndexChanged);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApply.BackColor = System.Drawing.Color.Aqua;
            this.btnApply.Location = new System.Drawing.Point(474, 368);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(152, 53);
            this.btnApply.TabIndex = 9;
            this.btnApply.Text = "Apply changes";
            this.btnApply.UseVisualStyleBackColor = false;
            // 
            // FrmSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 796);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmSetup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Setup";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ComboBox ddlColorButton;
        private System.Windows.Forms.Label lblTestFont;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ddlFont;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlColorExample;
    }
}