using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UNET_Classes;

namespace UNET_Theming
{
    public class Theming
    {

        public UNETTheme Theme;
        //colors
        public static Color Whiteforecolor;
        public static Color Extinguished;
        public static Color ExerciseSelectedButton;
        public static Color TraineeSelectedButton;
        public static Color TraineeNotSelectedButton;
        public static Color RoleSelectedButton;
        public static Color RoleNotSelectedButton;
        public static Color RadioSelectedButton;

        public static Color IntercomPressed;
        public static Color IntercomNotPressed;
        public static Color ButtonText;

        /// <summary>
        /// Set the general colors of the unettrainer          
        /// </summary>
        /// <param name="_theme"></param>
        public void SetTheme(UNETTheme _theme, Control _parent)
        {
            switch (RegistryAccess.GetStringRegistryValue(@"UNET", @"theme", "dark"))
            {
                case "dark":
                default:
                    {
                        Theme = UNETTheme.utDark;
                        Whiteforecolor = Color.White;
                        Extinguished = Color.DimGray;
                        ExerciseSelectedButton = Color.CadetBlue;
                        TraineeSelectedButton = Color.BlanchedAlmond;
                        TraineeNotSelectedButton = Color.LightGray;
                        RoleSelectedButton = Color.SandyBrown;
                        RoleNotSelectedButton = Color.LightGray; //zie parg 2.4.3
                        RadioSelectedButton = Color.SaddleBrown; // zie parg 2.4.3
                        IntercomNotPressed = Color.DimGray;
                        IntercomPressed = Color.LightGreen;
                        ButtonText = Color.Black;
                        break;
                    }
                case "light":
                    {
                        Theme = UNETTheme.utLight;
                        Whiteforecolor = Color.White;
                        Extinguished= Color.LightGray;
                        ExerciseSelectedButton = Color.LightBlue;
                        TraineeSelectedButton = Color.BlanchedAlmond;
                        TraineeNotSelectedButton = Color.LightGray;
                        RoleNotSelectedButton = Color.LightGray; //zie parg 2.4.3
                        RoleSelectedButton = Color.SaddleBrown; // zie parg 2.4.3
                        RadioSelectedButton = Color.BlanchedAlmond;
                        IntercomNotPressed = Color.DimGray;
                        IntercomPressed = Color.LightGreen;
                        ButtonText = Color.Black;
                        break;
                    }
                case "blue":
                    {
                        Theme = UNETTheme.utBlue;
                        Whiteforecolor = Color.White;
                        Extinguished = Color.LightBlue;
                        ExerciseSelectedButton = Color.Blue;
                        TraineeSelectedButton = Color.BlanchedAlmond;
                        TraineeNotSelectedButton = Color.LightGray;
                        RoleNotSelectedButton = Color.LightGray; //zie parg 2.4.3
                        RoleSelectedButton = Color.SaddleBrown; // zie parg 2.4.3
                        RadioSelectedButton = Color.BlanchedAlmond;
                        IntercomNotPressed = Color.DimGray;
                        IntercomPressed = Color.LightGreen;
                        ButtonText = Color.Black;
                        break;
                    }
            }

            //todo: mbv deze theme ook daadwerkelijk hieronder andere kleuren maken

            //we willen de parent ZELF ook themen als het een form is..
            if (_parent.GetType().BaseType == typeof(System.Windows.Forms.Form))
            {
                ((Form)_parent).ForeColor = Whiteforecolor;
                ((Form)_parent).BackColor = Extinguished;
                SetFormSizeAndPosition((Form)_parent);
                ((Form)_parent).FormBorderStyle = FormBorderStyle.None;
            }

            if (_parent.GetType().BaseType.BaseType.BaseType == typeof(System.Windows.Forms.Form))
            {
                ((Form)_parent).ForeColor = Whiteforecolor;
                ((Form)_parent).BackColor = Color.DimGray;
            }

            //  this.ForeColor = Color.White;
            //  this.BackColor = Color.DimGray;


            //loop thrue the controls of the parent
            foreach (Control ctrl in _parent.Controls)
            {
                if (ctrl.GetType() == typeof(System.Windows.Forms.Form))
                {
                    ((Form)ctrl).ForeColor = Whiteforecolor;
                    ((Form)ctrl).BackColor = Color.DimGray;
                }

                if (ctrl.GetType() == typeof(System.Windows.Forms.Form))
                {
                    ((Form)ctrl).ForeColor = Whiteforecolor;
                    ((Form)ctrl).BackColor = Color.DimGray;
                }
                if (ctrl.GetType() == typeof(System.Windows.Forms.Panel))
                {
                    ((Panel)ctrl).ForeColor = Whiteforecolor;
                    ((Panel)ctrl).BackColor = Color.Gray;
                }

                if (ctrl.GetType() == typeof(System.Windows.Forms.GroupBox))
                {
                    ((GroupBox)ctrl).ForeColor = Whiteforecolor;
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
                    if (((Button)ctrl).Name.ToLower().Contains("options") || ((Button)ctrl).Name.ToLower().Contains("mic"))
                    {
                        ((Button)ctrl).ForeColor = Color.Black;
                      //  ((Button)ctrl).BackColor = Color.Peru;
                    }
                    else
                    if (((Button)ctrl).Name.ToLower().Contains("trainee"))
                    {
                        ((Button)ctrl).ForeColor = ButtonText;
                    //    ((Button)ctrl).BackColor = Color.Peru;
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
                        ((Button)ctrl).ForeColor = ButtonText;
                 //       ((Button)ctrl).BackColor = Color.DeepSkyBlue;
                    }
                    else
                    if (((Button)ctrl).Name.ToLower().Contains("noise"))
                    {
                        ((Button)ctrl).ForeColor = Whiteforecolor;
                        ((Button)ctrl).BackColor = Color.DeepSkyBlue;
                    }
                    else
                    if (((Button)ctrl).Name.ToLower().Contains("mainpage") ||
                       ((Button)ctrl).Name.ToLower().Contains("il")
                        )
                    {
                        ((Button)ctrl).ForeColor = Whiteforecolor;
                        ((Button)ctrl).BackColor = Color.Gray;
                    }
                    else
                    if (((Button)ctrl).Name.ToLower().Contains("assist") ||
                       ((Button)ctrl).Name.ToLower().Contains("intercom") ||
                       ((Button)ctrl).Name.ToLower().Contains("audio")
                        )
                    {
                        ((Button)ctrl).ForeColor = Color.Black;
                        ((Button)ctrl).BackColor = Color.Aqua;
                    }


                }
                SetTheme(_theme, ctrl);
            }
        }

        public void SetFormSizeAndPosition(Control _control)
        {
            // StartPosition was set to FormStartPosition.Manual in the properties window.

            Rectangle screen = new Rectangle(new Point(500, 500), new Size(800, 600));
            int w = _control.Width >= screen.Width ? screen.Width : (screen.Width + _control.Width) / 2;
            int h = _control.Height >= screen.Height ? screen.Height : (screen.Height + _control.Height) / 2;
            //    _control.Location = new Point((screen.Width - w) / 2, (screen.Height - h) / 2);
            //    _control.Size = new Size(w, h);

            _control.Location = new Point(0, 0);
            _control.Size = new Size(800, 600);

        }


        /// <summary>
        /// at the start of the screen, all buttons should be invisible
        /// </summary>
        /// <param name="_panel"></param>
        public void InitPanels(Panel _panel)
        {
            foreach (Control c in _panel.Controls)
            {
                if (c.GetType() == typeof(Button))
                {
                    if (((Button)c).Name != "btnClose")
                        ((Button)c).Visible = false;

                }
            }

        }

    }
}
