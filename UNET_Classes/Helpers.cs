using System;
using System.Windows.Forms;
using System.Linq;

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
                    if ((c is Button) && (((Button)c).Enabled == false))
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
                int buttonstop = 22;
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
                        //  ((Button)(_panel.Controls[controlindex])).Enabled = true;
                        verttotal += ((Button)(but)).Height;
                        buttonleft += squaresize; //tel de breedte van 1 button op bij de left, voor de volgende



                        controlindex++;
                    }
                    Application.DoEvents();
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
                        if ((c is Button) && (((Button)c).Enabled == false))
                        {
                            c.Visible = false;
                        }

                    }
                    //daarna bereken de beschikbare verticale ruimte; 
                    int buttonheight = Convert.ToInt16((_panel.Height - 22) / _numberOfButtons);
                    int buttonstop = 0;

                    //bouw dan de grid op..
                    foreach(var but in _panel.Controls.OfType<Button>().Where(t => t.Enabled).OrderBy(x => x.Name))
                    {
                        ((Button)(but)).Visible = true;
                        ((Button)(but)).Top = buttonstop + 22;
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
