namespace SIM2UNET
{
    partial class FrmSIM2UNETMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSIM2UNETMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStartListening = new System.Windows.Forms.Button();
            this.tbxServer = new System.Windows.Forms.TextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStartListening);
            this.groupBox1.Controls.Add(this.tbxServer);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox1.Location = new System.Drawing.Point(29, 42);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(440, 126);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server";
            // 
            // btnStartListening
            // 
            this.btnStartListening.BackColor = System.Drawing.Color.Black;
            this.btnStartListening.ForeColor = System.Drawing.Color.Transparent;
            this.btnStartListening.ImageIndex = 0;
            this.btnStartListening.ImageList = this.imageList1;
            this.btnStartListening.Location = new System.Drawing.Point(357, 38);
            this.btnStartListening.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStartListening.Name = "btnStartListening";
            this.btnStartListening.Size = new System.Drawing.Size(51, 50);
            this.btnStartListening.TabIndex = 39;
            this.btnStartListening.UseVisualStyleBackColor = false;
            this.btnStartListening.Click += new System.EventHandler(this.btnStartListening_Click);
            // 
            // tbxServer
            // 
            this.tbxServer.BackColor = System.Drawing.Color.White;
            this.tbxServer.Location = new System.Drawing.Point(25, 52);
            this.tbxServer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbxServer.Name = "tbxServer";
            this.tbxServer.ReadOnly = true;
            this.tbxServer.Size = new System.Drawing.Size(304, 22);
            this.tbxServer.TabIndex = 38;
            this.tbxServer.Text = "192.168.0.10";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "play_32.png");
            this.imageList1.Images.SetKeyName(1, "stop32.png");
            // 
            // FrmSIM2UNETMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(853, 680);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmSIM2UNETMain";
            this.Text = "SIM2UNET";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStartListening;
        private System.Windows.Forms.TextBox tbxServer;
        private System.Windows.Forms.ImageList imageList1;
    }
}

