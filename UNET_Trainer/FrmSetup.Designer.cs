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
            this.btnApplyColors = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTestFont
            // 
            this.lblTestFont.AutoSize = true;
            this.lblTestFont.Location = new System.Drawing.Point(285, 174);
            this.lblTestFont.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTestFont.Name = "lblTestFont";
            this.lblTestFont.Size = new System.Drawing.Size(49, 13);
            this.lblTestFont.TabIndex = 22;
            this.lblTestFont.Text = "Test font";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 151);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Font";
            // 
            // ddlFont
            // 
            this.ddlFont.FormattingEnabled = true;
            this.ddlFont.Location = new System.Drawing.Point(50, 170);
            this.ddlFont.Margin = new System.Windows.Forms.Padding(2);
            this.ddlFont.Name = "ddlFont";
            this.ddlFont.Size = new System.Drawing.Size(223, 21);
            this.ddlFont.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 87);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Color of buttons";
            // 
            // pnlColorExample
            // 
            this.pnlColorExample.Location = new System.Drawing.Point(283, 106);
            this.pnlColorExample.Margin = new System.Windows.Forms.Padding(2);
            this.pnlColorExample.Name = "pnlColorExample";
            this.pnlColorExample.Size = new System.Drawing.Size(26, 22);
            this.pnlColorExample.TabIndex = 18;
            // 
            // ddlColorButton
            // 
            this.ddlColorButton.FormattingEnabled = true;
            this.ddlColorButton.Location = new System.Drawing.Point(50, 106);
            this.ddlColorButton.Margin = new System.Windows.Forms.Padding(2);
            this.ddlColorButton.Name = "ddlColorButton";
            this.ddlColorButton.Size = new System.Drawing.Size(223, 21);
            this.ddlColorButton.TabIndex = 17;
            // 
            // btnApplyColors
            // 
            this.btnApplyColors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApplyColors.BackColor = System.Drawing.Color.Aqua;
            this.btnApplyColors.Location = new System.Drawing.Point(50, 448);
            this.btnApplyColors.Margin = new System.Windows.Forms.Padding(2);
            this.btnApplyColors.Name = "btnApplyColors";
            this.btnApplyColors.Size = new System.Drawing.Size(114, 43);
            this.btnApplyColors.TabIndex = 23;
            this.btnApplyColors.Text = "Apply changes";
            this.btnApplyColors.UseVisualStyleBackColor = false;
            // 
            // FrmSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 647);
            this.Controls.Add(this.btnApplyColors);
            this.Controls.Add(this.lblTestFont);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ddlFont);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlColorExample);
            this.Controls.Add(this.ddlColorButton);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "FrmSetup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Setup";
            this.Controls.SetChildIndex(this.ddlColorButton, 0);
            this.Controls.SetChildIndex(this.pnlColorExample, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.ddlFont, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.lblTestFont, 0);
            this.Controls.SetChildIndex(this.btnApplyColors, 0);
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
        private System.Windows.Forms.Button btnApplyColors;
    }
}