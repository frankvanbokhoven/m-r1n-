using System;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;
using UNET_Button;

namespace UNET_Classes
{
   public static class Helpers
   {
        #region resize buttons
        /// <summary>
        /// Resize the panels, depending on the number of panels requested. Panels that are not nessecary, do not have to be visible and
        /// their space can be used for the other panels.
        /// todo: extentiemethod van maken
        /// </summary>
        public static void ResizeButtons(Panel _panel, int _numberOfButtons, string _group)
        {
            try
            {
                //begin met alle controls op invisible te zetten.
                foreach (Control c in _panel.Controls)
                {
                    //  if ((c is Button) && (((Button)c).Enabled == false))
                    //  {
                    //      c.Visible = false;
                    //  }

                    if (c is Button)
                    {
                        if (((Button)c).Tag != "enable")
                        {
                            if (!c.Name.Contains("Close"))
                            {

                                c.Visible = false;
                            }
                        }
                        else
                        {
                            c.Visible = true;
                        }
                    }

                    if (c is UNET_Button.UNET_Button)
                    {
                        if (((UNET_Button.UNET_Button)c).Tag != "enable")
                        {
                            if (!c.Name.Contains("Close"))
                            {

                                c.Visible = false;
                            }
                        }
                        else
                        {
                            c.Visible = true;
                        }
                    }

                }

                //daarna bereken de beschikbare ruimte; er zijn twee situaties: een vierkant panel of een verticaal panel
                int squareroot = Convert.ToInt16(Math.Sqrt(_numberOfButtons)) + 1; //rond dit getal naar beneden af
                int squaresize = _panel.Width / squareroot;
                int controlindex = 0;
                int buttonstop = 25;
                int verttotal = 0;
                int buttonleft = 0;

                //bouw dan de grid op, van links naar rechts en van boven naar onder
                foreach (var but in _panel.Controls.OfType<Button>().Where(t => t.Enabled).OrderBy(x => x.Name))
                {
                    if (!((Button)but).Name.Contains("Close"))
                    {
                        if (controlindex == squareroot)
                        {
                            buttonstop = buttonstop + squaresize;
                            buttonleft = 0;
                        }
                        ((Button)(but)).Visible = true;
                        ((Button)(but)).Top = buttonstop;
                        ((Button)(but)).Left = buttonleft;
                        ((Button)(but)).Width = squaresize;
                        ((Button)(but)).Height = squaresize;
                        verttotal += ((Button)(but)).Height;
                        buttonleft += squaresize; //tel de breedte van 1 button op bij de left, voor de volgende



                        controlindex++;
                    }
                    Application.DoEvents();
                }

                //bouw dan de grid op, van links naar rechts en van boven naar onder
                foreach (var but in _panel.Controls.OfType<UNET_Button.UNET_Button>().Where(t => t.Enabled).OrderBy(x => x.Name))
                {
                    if (!((UNET_Button.UNET_Button)but).Name.Contains("Close"))
                    {
                        if (controlindex == squareroot)
                        {
                            buttonstop = buttonstop + squaresize;
                            buttonleft = 0;
                        }
                        ((UNET_Button.UNET_Button)(but)).Visible = true;
                        ((UNET_Button.UNET_Button)(but)).Top = buttonstop;
                        ((UNET_Button.UNET_Button)(but)).Left = buttonleft;
                        ((UNET_Button.UNET_Button)(but)).Width = squaresize;
                        ((UNET_Button.UNET_Button)(but)).Height = squaresize;
                        verttotal += ((UNET_Button.UNET_Button)(but)).Height;
                        buttonleft += squaresize; //tel de breedte van 1 button op bij de left, voor de volgende



                        controlindex++;
                    }
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                string errormessage = ex.Message;
                // throw;
            }
        }

        /// <summary>
        /// Return only the numbers in a given string
        /// https://stackoverflow.com/questions/11002527/extract-numbers-from-string-to-create-digit-only-string
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static string ExtractNumber(string _original)
        {
            return new string(_original.Where(c => Char.IsDigit(c)).ToArray());
        }


        /// <summary>
        /// Resize the panels, depending on the number of panels requested. Panels that are not nessecary, do not have to be visible and
        /// their space can be used for the other panels.
        /// </summary>
        public static void ResizeButtonsVertical(Panel _panel, int _numberOfButtons, string _group)
        {
            if (_numberOfButtons > 0)
            {
                try
                {
                    //begin met alle controls op invisible te zetten.
                    foreach (Control c in _panel.Controls)
                    {
                        //  if ((c is Button) && (((Button)c).Enabled == false))
                        //  {
                        //      c.Visible = false;
                        //  }

                        if (c is Button)
                        {
                            if (((Button)c).Tag != "enable")
                            {
                                c.Visible = false;
                            }
                            else
                            {
                                c.Visible = true;
                            }
                        }

                    }
                    //daarna bereken de beschikbare verticale ruimte; 
                    int buttonheight = Convert.ToInt16((_panel.Height - 25) / _numberOfButtons);
                    int buttonstop = 0;

                    //bouw dan de grid op..
                    foreach(var but in _panel.Controls.OfType<Button>().Where(t => t.Enabled).OrderBy(x => x.Name))
                    {
                        ((Button)(but)).Visible = true;
                        ((Button)(but)).Top = buttonstop + 25;
                        ((Button)(but)).Left = 2;
                        ((Button)(but)).Width = _panel.Width - 4;
                        ((Button)(but)).Height = buttonheight - 2;
                        buttonstop += ((Button)(but)).Height;

                        Application.DoEvents();
                    }
                 }
                catch (Exception)
                {
                  //  log.Error("Error SetPanels", ex);
                    // throw;
                }
            }
        }
        #endregion

     
    }
}
