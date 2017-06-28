namespace UNET_Trainer
{
    partial class FrmUNETbaseSub
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
            this.btnMainPage = new System.Windows.Forms.Button();
            this.btnServiceRequest = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMainPage
            // 
            this.btnMainPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMainPage.BackColor = System.Drawing.Color.Aqua;
            this.btnMainPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMainPage.Location = new System.Drawing.Point(12, 495);
            this.btnMainPage.Name = "btnMainPage";
            this.btnMainPage.Size = new System.Drawing.Size(152, 53);
            this.btnMainPage.TabIndex = 7;
            this.btnMainPage.Text = "Main Page";
            this.btnMainPage.UseVisualStyleBackColor = false;
            this.btnMainPage.Click += new System.EventHandler(this.btnMainPage_Click);
            this.btnMainPage.Resize += new System.EventHandler(this.btnMainPage_Resize);
            // 
            // btnServiceRequest
            // 
            this.btnServiceRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnServiceRequest.BackColor = System.Drawing.Color.Aqua;
            this.btnServiceRequest.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnServiceRequest.Location = new System.Drawing.Point(170, 495);
            this.btnServiceRequest.Name = "btnServiceRequest";
            this.btnServiceRequest.Size = new System.Drawing.Size(152, 53);
            this.btnServiceRequest.TabIndex = 8;
            this.btnServiceRequest.Text = "Service Request";
            this.btnServiceRequest.UseVisualStyleBackColor = false;
            this.btnServiceRequest.Click += new System.EventHandler(this.btnServiceRequest_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(743, 51);
            this.panel1.TabIndex = 9;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(7, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(92, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "lblTitle";
            // 
            // FrmUNETbaseSub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 559);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnServiceRequest);
            this.Controls.Add(this.btnMainPage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmUNETbaseSub";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmUNETbaseSub";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmUNETbaseSub_Load);
            this.Shown += new System.EventHandler(this.FrmUNETbaseSub_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMainPage;
        private System.Windows.Forms.Button btnServiceRequest;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lblTitle;
    }
}