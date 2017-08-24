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
        protected UNETTheme Theme = UNETTheme.utDark;//dit zet de kleuren van de trainer

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
            // Set the text displayed in the caption.
            this.Text = "UNET";
            this.BackColor = Color.White;
            // Set the opacity to 75%.
            this.Opacity = 1;
            // Size the form to be 300 pixels in height and width.
            this.Size = new Size(800, 600);
            // Display the form in the center of the screen.
            // this.StartPosition = FormStartPosition.
            //   SetFormSizeAndPosition();
            this.Top = 0;
            this.Left = 0;
            this.Height = 600;
            this.Width = 800;
            SetTheme(Theme, this);
        }

        #region theme

        /// <summary>
        /// Set the colors of the
        /// </summary>
        /// <param name="_theme"></param>
        protected void SetTheme(UNETTheme _theme, Control _parent)
        {
            //we willen de parent ZELF ook themen als het een form is..
            if (_parent.GetType().BaseType.BaseType == typeof(System.Windows.Forms.Form))
            {
                ((Form)_parent).ForeColor = Color.White;
                ((Form)_parent).BackColor = Color.DimGray;
            }

            //loop thrue the controls of the parent
            foreach (Control ctrl in _parent.Controls)
            {
                if (ctrl.GetType() == typeof(System.Windows.Forms.Form))
                {
                    ((Form)ctrl).ForeColor = Color.White;
                    ((Form)ctrl).BackColor = Color.DimGray;
                }
                if (ctrl.GetType() == typeof(System.Windows.Forms.GroupBox))
                {
                    ((GroupBox)ctrl).ForeColor = Color.White;
                    ((GroupBox)ctrl).BackColor = Color.Gray;
                }



                if (ctrl.GetType() == typeof(System.Windows.Forms.Button))
                {
                    if (((Button)ctrl).Name.ToLower().Contains("radio"))
                    {
                        ((Button)ctrl).ForeColor = Color.Black;
                        ((Button)ctrl).BackColor = Color.DarkKhaki;
                    }
                    else
                    if (((Button)ctrl).Name.ToLower().Contains("close"))
                    {
                        ((Button)ctrl).ForeColor = Color.Black;
                        ((Button)ctrl).BackColor = Color.Red;
                    }
                    else
                    if (((Button)ctrl).Name.ToLower().Contains("trainee"))
                    {
                        ((Button)ctrl).ForeColor = Color.Black;
                        ((Button)ctrl).BackColor = Color.Peru;
                    }
                    else
                    if (((Button)ctrl).Name.ToLower().Contains("exersise"))
                    {
                        ((Button)ctrl).ForeColor = Color.Black;
                        ((Button)ctrl).BackColor = Color.LimeGreen;
                    }
                    else
                    if (((Button)ctrl).Name.ToLower().Contains("role"))
                    {
                        ((Button)ctrl).ForeColor = Color.Black;
                        ((Button)ctrl).BackColor = Color.DeepSkyBlue;
                    }
                    else
                    if (((Button)ctrl).Name.ToLower().Contains("noise"))
                    {
                        ((Button)ctrl).ForeColor = Color.White;
                        ((Button)ctrl).BackColor = Color.DeepSkyBlue;
                    }

                    if (((Button)ctrl).Name.ToLower().Contains("audio") ||
                         ((Button)ctrl).Name.ToLower().Contains("assist") ||
                         ((Button)ctrl).Name.ToLower().Contains("intercom"))
                    {
                        ((Button)ctrl).ForeColor = Color.Black;
                        ((Button)ctrl).BackColor = Color.Gray;
                    }

                    //   else
                    //   {
                    //       ((Button)ctrl).ForeColor = Color.White;
                    //       ((Button)ctrl).BackColor = Color.DimGray;
                    //   }
                }
                SetTheme(_theme, ctrl);
            }
        }
        #endregion

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
