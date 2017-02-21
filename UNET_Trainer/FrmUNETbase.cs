using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UNET_Trainer
{
    public partial class FrmUNETbase : Form
    {
        public FrmUNETbase()
        {
            InitializeComponent();
        }

        private void FrmUNETMain_Load(object sender, EventArgs e)
        {
            // Set the text displayed in the caption.
            this.Text = "UNET";
            this.BackColor = Color.White;
            // Set the opacity to 75%.
            this.Opacity = 1;
            // Size the form to be 300 pixels in height and width.
            this.Size = new Size(800, 600);
            // Display the form in the center of the screen.
            // this.StartPosition = FormStartPosition.Manual
            SetFormSizeAndPosition();
        }

        private void SetFormSizeAndPosition()
        {
            // StartPosition was set to FormStartPosition.Manual in the properties window.
        //    Rectangle screen = Screen.PrimaryScreen.WorkingArea;

            Rectangle screen = new Rectangle(new Point(500, 500), new Size(800, 600));
            int w = Width >= screen.Width ? screen.Width : (screen.Width + Width) / 2;
            int h = Height >= screen.Height ? screen.Height : (screen.Height + Height) / 2;
            this.Location = new Point((screen.Width - w) / 2, (screen.Height - h) / 2);
            this.Size = new Size(w, h);
        }
    }
}
