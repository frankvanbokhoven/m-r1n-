using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UNET_Classes;

namespace UNET_Trainer_Trainee
{
    public partial class FrmUNETbase : Form
    {
   
        public FrmUNETbase()
        {
            InitializeComponent();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        private void FrmUNETbase_Load(object sender, EventArgs e)
        {

        }

      

        #region dragformbypanel
        ///// <summary>
        ///// In testscenarios, we want to be able to drag the client to another position on the screen, but the unet is borderless
        ///// the code in this region causes the entire form to be draggable
        ///// https://stackoverflow.com/questions/1592876/make-a-borderless-form-movable
        ///// </summary>
        //private void SetFormSizeAndPosition()
        //{
        //    // StartPosition was set to FormStartPosition.Manual in the properties window.
        //   //    Rectangle screen = Screen.PrimaryScreen.WorkingArea;

        //    Rectangle screen = new Rectangle(new Point(500, 500), new Size(800, 600));
        //    int w = Width >= screen.Width ? screen.Width : (screen.Width + Width) / 2;
        //    int h = Height >= screen.Height ? screen.Height : (screen.Height + Height) / 2;
        //    this.Location = new Point((screen.Width - w) / 2, (screen.Height - h) / 2);
        //    this.Size = new Size(w, h);
        //}


        /// <summary>
        /// Dit zorgt dat het form alsnog te verslepen is door het met de rechter muisklik overal op het form te klikken
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmUNETbase_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion

  
    }
}
