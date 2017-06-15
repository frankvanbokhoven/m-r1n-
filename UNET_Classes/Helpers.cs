using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;

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
                    if (c is Button)
                    {
                        if (!c.Name.Contains("Close"))
                        {
                            c.Visible = false;
                        }
                    }
                }
                //daarna bereken de beschikbare ruimte; er zijn twee situaties: een vierkant panel of een verticaal panel
                int squareroot = Convert.ToInt16(Math.Sqrt(_numberOfButtons)) + 1; //rond dit getal naar beneden af
                int squaresize = _panel.Width / squareroot;
                int controlindex = 0;
                int buttonstop = 0;

                //bouw dan de grid op, van links naar rechts en van boven naar onder
                for (int i = 1; i <= squareroot; i++) // van links naar rechts
                {
                    int verttotal = 0;
                    int buttonleft = 0;
                    for (int j = 1; j <= squareroot; j++) // van boven naar onder
                    {
                        if (controlindex <= _numberOfButtons)
                        {
                            if ((_panel.Controls[controlindex] is Button) && ((_panel.Controls[controlindex].Name.ToLower().Contains(_group.ToLower())))) //het moet wel een button zijn
                            {
                                ((Button)(_panel.Controls[controlindex])).Visible = true;
                                ((Button)(_panel.Controls[controlindex])).Top = buttonstop + 22;
                                ((Button)(_panel.Controls[controlindex])).Left = buttonleft;
                                ((Button)(_panel.Controls[controlindex])).Width = squaresize;
                                ((Button)(_panel.Controls[controlindex])).Height = squaresize;
                                verttotal += ((Button)(_panel.Controls[controlindex])).Height;
                                buttonleft += squaresize; //tel de breedte van 1 button op bij de left, voor de volgende

                                Application.DoEvents();
                            }
                            controlindex++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    buttonstop += squaresize;
                }
            }
            catch (Exception)
            {
                //log.Error("Error SetPanels", ex);
                // throw;
            }
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
                        if (c is Button)
                        {
                            c.Visible = false;
                        }
                    }
                    //daarna bereken de beschikbare verticale ruimte; 
                    int buttonheight = Convert.ToInt16((_panel.Height - 22) / _numberOfButtons);
                    int buttonstop = 0;

                    //bouw dan de grid op..
                    for (int i = 1; i <= _numberOfButtons; i++) // van boven naar onder
                    {
                        if (i <= _numberOfButtons)
                        {
                            if ((_panel.Controls[i] is Button) && ((_panel.Controls[i].Name.ToLower().Contains(_group.ToLower())))) //het moet wel een button zijn
                            {
                                ((Button)(_panel.Controls[i])).Visible = true;
                                ((Button)(_panel.Controls[i])).Top = buttonstop + 22;
                                ((Button)(_panel.Controls[i])).Left = 2;
                                ((Button)(_panel.Controls[i])).Width = _panel.Width - 4;
                                ((Button)(_panel.Controls[i])).Height = buttonheight - 2;
                                buttonstop += ((Button)(_panel.Controls[i])).Height;

                                Application.DoEvents();
                            }
                        }
                        else
                        {
                            break;
                        }
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
