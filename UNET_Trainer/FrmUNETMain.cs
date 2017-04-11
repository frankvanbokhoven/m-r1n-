﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UNET_Trainer.UNET_Service;

namespace UNET_Trainer
{


    public partial class FrmUNETMain : FrmUNETbase
    {
        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Boolean Muted = false;
        private Boolean MonitorTrainee = false;
        private Boolean MonitorRadio = false;
        
        bool[] MonitorTraineeArray = new bool[16]; //this array holds the monitor status of the trainees
        bool[] MonitorRadioArray = new bool[20]; //this array holds the monitor status of the Radios
        bool[] ExerciseArray = new bool[9]; //this array holds the exercise status
        private static FrmUNETMain inst;
        public static FrmUNETMain GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new FrmUNETMain();
                return inst;
            }
        }

        public FrmUNETMain()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            FrmRoles frm = new FrmRoles();
            frm.Show();
        }

        private void btnTrainees_Click(object sender, EventArgs e)
        {
            FrmTrainees frm = new FrmTrainees();
            frm.Show();
        }

        private void btnAudio_Click(object sender, EventArgs e)
        {
            FrmAudio frm = new FrmAudio();
            frm.Show();
        }

        private void lblSetup_Click(object sender, EventArgs e)
        {
            FrmSetup frm = new FrmSetup();
            frm.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            SetButtonStatus(this);
        }

        #region Status setters

        /// <summary>
        /// This routine sets the statusled of each button, depending on its status
        /// It also enables/disables buttons based on the number of exercises given bij the service
        /// </summary>
        private void SetButtonStatus(Control parent)
        {
            // first the trainees, we assume the name of the button component is the key for the function
            foreach (Control c in parent.Controls)
            {
                if (c.GetType() == typeof(Button) && (c.Name.ToLower().Contains("trainee")))
                {
                    if (((Button)c).ImageIndex == 1)
                    {
                        ((Button)c).ImageIndex = 2;
                    }
                    else
                    { ((Button)c).ImageIndex = 1; }
                }

                if (c.GetType() == typeof(Button) && (c.Name.ToLower().Contains("exersise")))
                {
                    if (((Button)c).ImageIndex == 1)
                    {
                        ((Button)c).ImageIndex = 2;
                    }
                    else
                    { ((Button)c).ImageIndex = 1; }
                }

                if (c.GetType() == typeof(Button) && (c.Name.ToLower().Contains("role")))
                {
                    if (((Button)c).ImageIndex == 1)
                    {
                        ((Button)c).ImageIndex = 2;
                    }
                    else
                    { ((Button)c).ImageIndex = 1; }
                }
                else
                {
                    SetButtonStatus(c);
                } 
            }

            try
            {
                // we mocken hier een aantal exercises. Als er bijv. 5 in de combobox staat, worden hier 5 exercises gemaakt
                using (UNET_Service.Service1Client service = new UNET_Service.Service1Client())
                {
                    service.Open();
                 
                    //enable the Exercise buttons
                    var resultlist = service.GetExercises();
                    List<Exercise> lst = resultlist.ToList<Exercise>(); //C# v3 manier om een array in een list te krijgen

                    btnExersise01.Enabled = lst.Count > 1;
                    btnExersise02.Enabled = lst.Count > 2;
                    btnExersise03.Enabled = lst.Count > 3;
                    btnExersise04.Enabled = lst.Count > 4;
                    btnExersise05.Enabled = lst.Count > 5;
                    btnExersise06.Enabled = lst.Count > 6;
                    btnExersise07.Enabled = lst.Count > 7;
                    btnExersise08.Enabled = lst.Count > 8;


                    //enable the Roles buttons
                    var rolelist = service.GetRoles();
                    List<Role> lstrole = rolelist.ToList<Role>(); //C# v3 manier om een array in een list te krijgen

                    btnRole1.Enabled = lstrole.Count > 1;
                    btnRole2.Enabled = lstrole.Count > 2;
                    btnRole3.Enabled = lstrole.Count > 3;
                    btnRole4.Enabled = lstrole.Count > 4;
                    btnRole5.Enabled = lstrole.Count > 5;
                    btnRole6.Enabled = lstrole.Count > 6;
                    btnRole7.Enabled = lstrole.Count > 7;
                    btnRole8.Enabled = lstrole.Count > 8;
                    btnRole9.Enabled = lstrole.Count > 9;
                    btnRole10.Enabled = lstrole.Count > 10;
                    btnRole11.Enabled = lstrole.Count > 11;
                    btnRole12.Enabled = lstrole.Count > 12;
                    btnRole13.Enabled = lstrole.Count > 13;
                    btnRole14.Enabled = lstrole.Count > 14;
                    btnRole15.Enabled = lstrole.Count > 15;
                    btnRole16.Enabled = lstrole.Count > 16;
                    btnRole17.Enabled = lstrole.Count > 17;
                    btnRole18.Enabled = lstrole.Count > 18;
                    btnRole19.Enabled = lstrole.Count > 19;
                    btnRole20.Enabled = lstrole.Count > 20;


                    service.Close();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error using WCF SetButtonStatus", ex);
                // throw;
            }
        }

        #endregion

        private void FrmUNETMain_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            this.Text = "UNET Instructor";
        }

        private void btnClassBroadcast_Click(object sender, EventArgs e)
        {
            FrmClassBroadcast frm = new FrmClassBroadcast();
            frm.Show();
        }

        private void btnRadios_Click(object sender, EventArgs e)
        {
            FrmRadioSetup frm = new FrmRadioSetup();
            frm.Show();
        }

        private void btnMute_Click(object sender, EventArgs e)
        {
            if (!Muted)
            {
                Muted = true;
                btnMute.BackColor = System.Drawing.Color.Red;
                btnMute.Text = "Muted";
            }
            else
            {
                Muted = false;
                btnMute.BackColor = System.Drawing.Color.LightGreen;
                btnMute.Text = "Mute radio";
            }
        }

        private void btnMonitorTrainee_Click(object sender, EventArgs e)
        {
            SetTraineeStatus();
            if (!MonitorTrainee)
            {
                MonitorTrainee = true;
                btnMonitorTrainee.BackColor = System.Drawing.Color.SaddleBrown;
                btnMonitorTrainee.ForeColor = System.Drawing.Color.White;
             }
            else
            {
                MonitorTrainee = false;
                btnMonitorTrainee.BackColor = System.Drawing.Color.Aqua;
                btnMonitorTrainee.ForeColor = System.Drawing.Color.Black;
            }
        }

        /// <summary>
        /// Resize the panels, depending on the number of panels requested. Panels that are not nessecary, do not have to be visible and
        /// their space can be used for the other panels.
        /// </summary>
        private void ResizePanels(Panel _panel, int _numberOfPanels)
        {
            //begin met alle controls op invisible te zetten.
            foreach (Button b in _panel.Controls)
            {
                b.Visible = false;
            }
            //get the number of panels
            int squareroot = Convert.ToInt16(Math.Sqrt(_numberOfPanels)) + 1; //rond dit getal naar beneden af
            int availablehorspace = _panel.Width / squareroot;
            int availablevertspace = _panel.Height / squareroot;
            int controlindex = 0;
            int buttonstop = 0;

            for (int i = 1; i <= squareroot; i++) // van boven naar onder
            {
                int buttonleft = 0;
                for (int j = 1; j <= squareroot; j++) // van links naar rechts
                {
                    if (controlindex < _numberOfPanels)
                    {
                        ((Button)(panel2.Controls[controlindex])).Visible = true;
                        ((Button)(panel2.Controls[controlindex])).Top = buttonstop;
                        ((Button)(panel2.Controls[controlindex])).Left = buttonleft;
                        ((Button)(panel2.Controls[controlindex])).Width = availablehorspace;
                        ((Button)(panel2.Controls[controlindex])).Height = availablehorspace;

                        controlindex++;
                        buttonleft += availablehorspace; //tel de breedte van 1 button op bij de left, voor de volgende
                    }
                    else
                    {
                        break;
                    }
                }

                buttonstop += availablevertspace;
            }
        }

        #region WCF events
        private void HandleWCFEvents()
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, String.Format("Error using WCF methods>{0}", ex.Message ));
                // throw;
            }
        }
        #endregion

        #region trainee events
        /// <summary>
        /// Hier wordt de kleur van de trainee knop weer teruggezet, alsmede de array
        /// </summary>
        private void SetTraineeStatus()
        {

            //loop thrue the Monitortraineearray to set the proper status
            for (int i = 0; i <= 15; i++)
            {
                MonitorTraineeArray[i] = false;
            }

            // A little amateur.. but it just is the fastest manner
            btnTraineeAA.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeBB.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeCC.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeDD.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeEE.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeFF.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeGG.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeHH.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeJJ.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeKK.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeLL.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeMM.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeNN.BackColor = System.Drawing.Color.LightGreen;
            btnTraineePP.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeRR.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeSS.BackColor = System.Drawing.Color.LightGreen;

            btnTraineeAA.ForeColor = System.Drawing.Color.Black;
            btnTraineeBB.ForeColor = System.Drawing.Color.Black;
            btnTraineeCC.ForeColor = System.Drawing.Color.Black;
            btnTraineeDD.ForeColor = System.Drawing.Color.Black;
            btnTraineeEE.ForeColor = System.Drawing.Color.Black;
            btnTraineeFF.ForeColor = System.Drawing.Color.Black;
            btnTraineeGG.ForeColor = System.Drawing.Color.Black;
            btnTraineeHH.ForeColor = System.Drawing.Color.Black;
            btnTraineeJJ.ForeColor = System.Drawing.Color.Black;
            btnTraineeKK.ForeColor = System.Drawing.Color.Black;
            btnTraineeLL.ForeColor = System.Drawing.Color.Black;
            btnTraineeMM.ForeColor = System.Drawing.Color.Black;
            btnTraineeNN.ForeColor = System.Drawing.Color.Black;
            btnTraineePP.ForeColor = System.Drawing.Color.Black;
            btnTraineeRR.ForeColor = System.Drawing.Color.Black;
            btnTraineeSS.ForeColor = System.Drawing.Color.Black;

        }

        /// <summary>
        /// When the button  'monitor trainee' clicked and after that one of the trainee buttons,
        /// this trainee button must be set to brown, and a possible other trainee button must be set to the default color
        /// this generic code covers this for all all buttons at once
        /// </summary>
        /// <param name="_btn"></param>
        private void SetStatusAndColorTraineeButtons(Button _btn)
        {
            if (MonitorTrainee)
            {
                // the trainee buttons are named e.g.: btnTraineeAA , we use this name, to find in the enum the index that is connected to this enum
                int traineeIndex = (int)(Enum.Parse(typeof(Enums.Trainees), _btn.Name.Remove(0, 3)));
                MonitorTraineeArray[traineeIndex] = true;
                _btn.BackColor = System.Drawing.Color.SaddleBrown;
                _btn.ForeColor = System.Drawing.Color.White;
            }
        }

        private void btnTraineeAA_Click(object sender, EventArgs e)
        {
            SetTraineeStatus();

            SetStatusAndColorTraineeButtons((Button)sender);
        }
        #endregion


        #region radio events

        /// <summary>
        /// Hier wordt de kleur van de radio knop weer teruggezet, alsmede de array
        /// </summary>
        private void SetRadioStatus()
        {

            //loop thrue the Monitortraineearray to set the proper status
            for (int i = 0; i <= 19; i++)
            {
                MonitorRadioArray[i] = false;
            }

            // A little amateur.. but it just is the fastest manner
            btnRadio01.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio02.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio03.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio04.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio05.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio06.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio07.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio08.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio09.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio10.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio11.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio12.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio13.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio14.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio15.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio16.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio17.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio18.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio19.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRadio20.BackColor = System.Drawing.Color.DarkSeaGreen;

            btnRadio01.ForeColor = System.Drawing.Color.Black;
            btnRadio02.ForeColor = System.Drawing.Color.Black;
            btnRadio03.ForeColor = System.Drawing.Color.Black;
            btnRadio04.ForeColor = System.Drawing.Color.Black;
            btnRadio05.ForeColor = System.Drawing.Color.Black;
            btnRadio06.ForeColor = System.Drawing.Color.Black;
            btnRadio07.ForeColor = System.Drawing.Color.Black;
            btnRadio08.ForeColor = System.Drawing.Color.Black;
            btnRadio09.ForeColor = System.Drawing.Color.Black;
            btnRadio10.ForeColor = System.Drawing.Color.Black;
            btnRadio11.ForeColor = System.Drawing.Color.Black;
            btnRadio12.ForeColor = System.Drawing.Color.Black;
            btnRadio13.ForeColor = System.Drawing.Color.Black;
            btnRadio14.ForeColor = System.Drawing.Color.Black;
            btnRadio15.ForeColor = System.Drawing.Color.Black;
            btnRadio16.ForeColor = System.Drawing.Color.Black;
            btnRadio17.ForeColor = System.Drawing.Color.Black;
            btnRadio18.ForeColor = System.Drawing.Color.Black;
            btnRadio19.ForeColor = System.Drawing.Color.Black;
            btnRadio20.ForeColor = System.Drawing.Color.Black;
        }


        private void btnMonitorRadio_Click(object sender, EventArgs e)
        {
            SetRadioStatus();
            if (!MonitorRadio)
            {
                MonitorRadio = true;
                btnMonitorRadio.BackColor = System.Drawing.Color.SaddleBrown;
                btnMonitorRadio.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                MonitorRadio = false;
                btnMonitorRadio.BackColor = System.Drawing.Color.Aqua;
                btnMonitorRadio.ForeColor = System.Drawing.Color.Black;
            }

        }      
      

        private void btnRadio01_Click(object sender, EventArgs e)
        {
            SetRadioStatus();

            SetStatusAndColorRadioButtons((Button)sender);
        }

        /// <summary>
        /// When the button  'monitor radio' clicked and after that one of the radio buttons,
        /// this radio button must be set to brown, and a possible other trainee button must be set to the default color
        /// this generic code covers this for all all buttons at once
        /// </summary>
        /// <param name="_btn"></param>
        private void SetStatusAndColorRadioButtons(Button _btn)
        {
            if (MonitorRadio)
            {
                // the trainee buttons are named e.g.: btnRadioAA , we use this name, to find in the enum the index that is connected to this enum
                int radioIndex = (int)(Enum.Parse(typeof(Enums.Radios), _btn.Name.Remove(0, 3)));
                MonitorRadioArray[radioIndex] = true;
                _btn.BackColor = System.Drawing.Color.SaddleBrown;
                _btn.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                //color should be green, described in paragraph 2.1.7
                _btn.BackColor = System.Drawing.Color.LimeGreen;
                _btn.ForeColor = System.Drawing.Color.Black;
            }
        }
        #endregion

        private void btnExersise01_Click(object sender, EventArgs e)
        {
            SetExerciseStatus();
            SetStatusAndColorExerciseButtons((Button)sender);

        }

        private void SetStatusAndColorExerciseButtons(Button _btn)
        {
            int exerciseIndex;
            // the trainee buttons are named e.g.: btnRadioAA , we use this name, to find in the enum the index that is connected to this enum
            if (_btn.Name.ToLower() != "btnil")
            {
                exerciseIndex = (int)(Enum.Parse(typeof(Enums.Exercises), _btn.Name.Remove(0, 3)));
            }
            else
            {
                exerciseIndex = 8;
            }
                ExerciseArray[exerciseIndex] = true;
                _btn.BackColor = System.Drawing.Color.SaddleBrown;
                _btn.ForeColor = System.Drawing.Color.White;
         }

        private void SetExerciseStatus()
        {
            //loop thrue the Exersise array to set the proper status
            for (int i = 0; i <= 8; i++)
            {
                ExerciseArray[i] = false;
            }

            //reset the colors of the exersise buttons
            btnExersise01.BackColor = System.Drawing.Color.Aqua;
            btnExersise02.BackColor = System.Drawing.Color.Aqua;
            btnExersise03.BackColor = System.Drawing.Color.Aqua;
            btnExersise04.BackColor = System.Drawing.Color.Aqua;
            btnExersise05.BackColor = System.Drawing.Color.Aqua;
            btnExersise06.BackColor = System.Drawing.Color.Aqua;
            btnExersise07.BackColor = System.Drawing.Color.Aqua;
            btnExersise08.BackColor = System.Drawing.Color.Aqua;
            btnIL.BackColor = System.Drawing.Color.Aqua;

            btnExersise01.ForeColor = System.Drawing.Color.Black;
            btnExersise02.ForeColor = System.Drawing.Color.Black;
            btnExersise03.ForeColor = System.Drawing.Color.Black;
            btnExersise04.ForeColor = System.Drawing.Color.Black;
            btnExersise05.ForeColor = System.Drawing.Color.Black;
            btnExersise06.ForeColor = System.Drawing.Color.Black;
            btnExersise07.ForeColor = System.Drawing.Color.Black;
            btnExersise08.ForeColor = System.Drawing.Color.Black;
            btnIL.ForeColor = System.Drawing.Color.Black;

        }
    }
}
