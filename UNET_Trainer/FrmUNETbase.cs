using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UNET_Classes;

namespace UNET_Trainer
{
  

    public partial class FrmUNETbase : Form
    {
        //log4net
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected UNETTheme Theme = UNETTheme.utDark;//dit zet de kleuren van de trainer

        [DllImport("user32.dll")]
        protected static extern IntPtr GetForegroundWindow();


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


            // //Set the general colors of the unettrainer
            // switch (ConfigurationManager.AppSettings["Theme"].ToString())
            // { 
            //     case "dark": { Theme = UNETTheme.utDark; break; }
            //     case "light": { Theme = UNETTheme.utLight; break; }
            //     case "blue": { Theme = UNETTheme.utBlue; break; }
            //     default: { Theme = UNETTheme.utDark; break; }
            // }
            //SetTheme(Theme, this);
        }

        /// <summary>
        /// Set the colors of the
        /// </summary>
        /// <param name="_theme"></param>
        protected void SetTheme(UNETTheme _theme, Control _parent)
        {
            //loop thrue the controls
            foreach (Control ctrl in _parent.Controls)
            {
                if (ctrl.GetType() == typeof(System.Windows.Forms.Form))
                {
                    ((Form)ctrl).ForeColor = Color.White;
                    ((Form)ctrl).BackColor = Color.DimGray;
                }
                if (ctrl.GetType() == typeof(System.Windows.Forms.Panel))
                {
                    ((Panel)ctrl).ForeColor = Color.White;
                    ((Panel)ctrl).BackColor = Color.DimGray;
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

                    //   else
                    //   {
                    //       ((Button)ctrl).ForeColor = Color.White;
                    //       ((Button)ctrl).BackColor = Color.DimGray;
                    //   }
                }
                SetTheme(_theme, ctrl);
            }
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


        /// <summary>
        /// Zorg dat de panels een witte border krijgen als ze een dargray opvulkleur hebben
          /// https://stackoverflow.com/questions/76455/how-do-you-change-the-color-of-the-border-on-a-group-box
      /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UC_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.White, ButtonBorderStyle.Solid);

        }
    }
}
