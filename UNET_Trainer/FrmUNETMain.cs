using System;
using System.Windows.Forms;

namespace UNET_Trainer
{
    public partial class FrmUNETMain : FrmUNETbase
    {

        private Boolean Muted = false;
        private Boolean MonitorTrainee = false;
        
        bool[] MonitorTraineeArray = new bool[16]; //this array holds the monitor status of the trainees
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
        }


        #endregion

        private void FrmUNETMain_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
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

        private void btnRole1_Click(object sender, EventArgs e)
        {

        }

        private void btnTraineeAA_Click(object sender, EventArgs e)
        {
            SetTraineeStatus();

            if (MonitorTrainee)
            {
                MonitorTraineeArray[0] = true;
                btnTraineeAA.BackColor = System.Drawing.Color.SaddleBrown;
            }
         }


        /// <summary>
        /// Hier wordt de kleur van de trainee knop weer teruggezet, alsmede de array
        /// </summary>
        private void SetTraineeStatus()
        { 

            //loop thrue the Monitortraineearray to set the proper status
            for (int i = 0; i <= 15; i++)
            {
                MonitorTraineeArray[i] = false;

            //    foreach (Control c in this.Controls)
            //        if (c.Name == Name)
            //            return c;
            //    //and also g
            //    ((Button)GetControlByName(string.Format("btnTrainee{0}", i))).BackColor = System.Drawing.Color.Aqua;
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
            btnTraineeRR.BackColor = System.Drawing.Color.LightGreen;
            btnTraineeSS.BackColor = System.Drawing.Color.LightGreen;
         

        }

        /// <summary>
        /// geeft het control object terug met een gegeven naam
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private  Control GetControlByName(string Name)
        {
            foreach (Control c in this.Controls)
                if (c.Name == Name)
                    return c;

            return null;
        }

        private void btnTraineeBB_Click(object sender, EventArgs e)
        {
            SetTraineeStatus();

            if (MonitorTrainee)
            {
                MonitorTraineeArray[1] = true;
                btnTraineeBB.BackColor = System.Drawing.Color.SaddleBrown;
            }
        }
    }
}
