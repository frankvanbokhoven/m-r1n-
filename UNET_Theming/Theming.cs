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
        //constants for colors
        public static Color cInActive = Color.Gray;
        public static Color cExtinguised = Color.DarkSeaGreen;
        public static Color cControlSelected = Color.DarkRed;
        public static Color cFontSelected = Color.White;
        public static Color cFontNotSelected = Color.Black;
        public static Color cBroadcasting = Color.Red;
        public static Color cFontBroadcasting = Color.White;

        //colors
        public static Color Whiteforecolor;
        public static Color Background;
        public static Color ExerciseNotSelected;
        public static Color ExerciseSelectedButton;
        public static Color ExerciseOtherSelectedButton; //color when the exercise is selected by another instructor (REQ__UNET_SRS_3 SRS 3.1.3.1.1)
        public static Color ExerciseOtherInstructorRoles; // REQ_UNET_SRS_4
        public static Color TraineeSelectedButton;
        public static Color TraineeNotSelectedButton;
        public static Color RoleSelectedButton;
        public static Color RolePendingCallButton;
        public static Color RoleNotSelectedButton;
        public static Color RadioSelectedButton;
        public static Color RadioNotSelectedButton;

        public static Color IntercomPressed;
        public static Color IntercomNotPressed;
        public static Color ButtonText;
        public static Color ButtonSelectedText;
        public static Color ButtonDarkSelectedText;
        public static Color AssistRequestedColor;
        public static Color AssistAcknowledgedColor;
        public static Color ILModeActive;
        public static Color ILModeInactive;

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
                        Background = Color.DimGray;
                        ExerciseNotSelected = Color.DimGray;
                        ExerciseSelectedButton = Color.Brown;
                        ExerciseOtherSelectedButton = Color.LightBlue;
                        ExerciseOtherInstructorRoles = Color.LightGreen;
                        TraineeSelectedButton = Color.PowderBlue;
                        TraineeNotSelectedButton = Color.LightGray;
                        RoleSelectedButton = Color.SandyBrown;
                        RoleNotSelectedButton = Color.LightGray; //zie parg 2.4.3
                        RolePendingCallButton = Color.Red;
                        RadioSelectedButton = Color.SaddleBrown; // zie parg 2.4.3
                        RadioNotSelectedButton = Color.DimGray;
                        IntercomNotPressed = Color.DimGray;
                        IntercomPressed = Color.LightGreen;
                        ButtonText = Color.Black;
                        ButtonSelectedText = Color.White;
                        ButtonDarkSelectedText = Color.Black;
                        AssistAcknowledgedColor = Color.Green;
                        AssistRequestedColor = Color.Gray;
                        ILModeActive = Color.Brown;
                        ILModeInactive = Color.DimGray;

                        break;
                    }
                case "light":
                    {
                        Theme = UNETTheme.utLight;
                        Whiteforecolor = Color.White;
                        Background = Color.WhiteSmoke;
                        ExerciseNotSelected= Color.LightGray;
                        ExerciseSelectedButton = Color.Brown;
                        ExerciseOtherSelectedButton = Color.LightBlue;
                        ExerciseOtherInstructorRoles = Color.LightGreen;
                        TraineeSelectedButton = Color.PowderBlue;
                        TraineeNotSelectedButton = Color.LightGray;
                        RoleNotSelectedButton = Color.LightGray; //zie parg 2.4.3
                        RoleSelectedButton = Color.SaddleBrown; // zie parg 2.4.3
                        RolePendingCallButton = Color.Red;
                        RadioSelectedButton = Color.BlanchedAlmond;
                        RadioNotSelectedButton = Color.DimGray;
                        IntercomNotPressed = Color.DimGray;
                        IntercomPressed = Color.LightGreen;
                        ButtonText = Color.Black;
                        ButtonSelectedText = Color.White;
                        AssistAcknowledgedColor = Color.Green;
                        AssistRequestedColor = Color.Gray;


                        break;
                    }
                case "blue":
                    {
                        Theme = UNETTheme.utBlue;
                        Whiteforecolor = Color.White;
                        Background = Color.WhiteSmoke;
                        ExerciseNotSelected = Color.LightBlue;
                        ExerciseSelectedButton = Color.Brown;
                        ExerciseOtherSelectedButton = Color.LightBlue;
                        ExerciseOtherInstructorRoles = Color.LightGreen;
                        TraineeSelectedButton = Color.PowderBlue;
                        TraineeNotSelectedButton = Color.LightGray;
                        RoleNotSelectedButton = Color.LightGray; //zie parg 2.4.3
                        RoleSelectedButton = Color.SaddleBrown; // zie parg 2.4.3
                        RolePendingCallButton = Color.Red;
                        RadioSelectedButton = Color.BlanchedAlmond;
                        RadioNotSelectedButton = Color.DimGray;
                        IntercomNotPressed = Color.DimGray;
                        IntercomPressed = Color.LightGreen;
                        ButtonText = Color.Black;
                        ButtonSelectedText = Color.White;


                        AssistAcknowledgedColor = Color.Green;
                        AssistRequestedColor = Color.Gray;


                        break;
                    }
            }

            //todo: mbv deze theme ook daadwerkelijk hieronder andere kleuren maken

            //we willen de parent ZELF ook themen als het een form is..
            if (_parent.GetType().BaseType == typeof(System.Windows.Forms.Form))
            {
                ((Form)_parent).ForeColor = Whiteforecolor;
                ((Form)_parent).BackColor = Background;
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
                       ((Button)ctrl).Name.ToLower().Contains("il") ||
                        ((Button)ctrl).Name.ToLower().Contains("service")
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
