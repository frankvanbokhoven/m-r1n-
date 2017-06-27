using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UNET_Tester.UNET_Service;
using ColorMine;
using System.Configuration;

namespace UNET_Tester
{
    public partial class frmUNETTester_Main : Form
    {
        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private UNET_Service.Service1Client service = new UNET_Service.Service1Client();

        public frmUNETTester_Main()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            cbxExercise.Text = ConfigurationManager.AppSettings["Exersise"];
            cbxRadios.Text = ConfigurationManager.AppSettings["Radio"];
            cbxRole.Text = ConfigurationManager.AppSettings["Role"];
            cbxTrainee.Text = ConfigurationManager.AppSettings["Trainee"];
            GetUNETStatus();

            timer1.Enabled = true;


            }

        /// <summary>
        /// add a line to the top of the listbox
        /// </summary>
        /// <param name="_item"></param>
        /// <param name="_color"></param>
        private void AddToListbox(string _item, [Optional] Color _color)
        {

            string additionDate = string.Format("{0}/{1}/{2}-{3}:{4}:{5}", DateTime.Now.Year.ToString("00"), DateTime.Now.Month.ToString("00"), DateTime.Now.Date.ToString("00"), DateTime.Now.Hour.ToString("00"), DateTime.Now.Minute.ToString("00"), DateTime.Now.Second.ToString("00"));


            if (_color != Color.White)
            {
                listBoxGetmethods.ForeColor = _color;
            }
            listBoxGetmethods.Items.Insert(0, (string.Format("{0} => {1}", additionDate, _item)));
            if (_color != Color.White)
            {
                listBoxGetmethods.ForeColor = Color.White;
            }

        }



        private void GetUNETStatus()
        {
            try
            {
                // we mocken hier een aantal exercises. Als er bijv. 5 in de combobox staat, worden hier 5 exercises gemaakt
                using (UNET_Service.Service1Client service = new UNET_Service.Service1Client())
                {
                    //Exercises
                    var resultlist = service.GetExercises();
                    List<Exercise> lst = resultlist.ToList<Exercise>(); //C# v3 manier om een array in een list te krijgen

                    foreach (Exercise exer in lst)
                    {
                        AddToListbox(string.Format("Exercise: {0}, Name: {1}", exer.Number, exer.SpecificationName));
                    }
                    cbxExercise.Text = lst.Count.ToString();

                    //Roles
                    var rolelist = service.GetRoles();
                    List<Role> lstrol = rolelist.ToList<Role>(); //C# v3 manier om een array in een list te krijgen

                    foreach (Role rol in lstrol)
                    {
                        AddToListbox(string.Format("Role: {0}, Name: {1}", rol.ID, rol.Name));
                    }
                    cbxRole.Text = lstrol.Count.ToString();

                    //Trainee
                    var traineelist = service.GetTrainees();
                    List<Trainee> lsttrainee = traineelist.ToList<Trainee>(); //C# v3 manier om een array in een list te krijgen

                    foreach (Trainee trainee in lsttrainee)
                    {
                        AddToListbox(string.Format("Trainee: {0}, Name: {1}", trainee.ID, trainee.Name));
                    }
                    cbxTrainee.Text = lsttrainee.Count.ToString();

                    //Radio
                    var radiolist = service.GetRadios();
                    List<Radio> lstRadio = radiolist.ToList<Radio>(); //C# v3 manier om een array in een list te krijgen

                    foreach (Radio radio in lstRadio)
                    {
                        AddToListbox(string.Format("Radio: {0}, Name: {1}", radio.ID, radio.Description));
                    }
                    cbxRadios.Text = lstRadio.Count.ToString();

                    ///updaten noiselevel
                    lbxNoiseLevel.Items.Clear();
                     int i = 0;
                    foreach (Radio radio in radiolist)
                    {
                        lbxNoiseLevel.Items.Add(string.Format("Radio {0} Noise: {1}", i, Convert.ToString(radio.NoiseLevel)));
                        i++;
                    }




                    service.Close();
                }

            }
            catch (Exception ex)
            {
                AddToListbox(String.Format("Error using WCF methods>{0}", ex.Message));
                log.Error("Error using WCF method change exercise", ex);
                // throw;
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                AddToListbox(string.Format("Set Exercises to: {0}", Convert.ToInt16(cbxExercise.SelectedValue)), Color.LimeGreen);
                // we mocken hier een aantal exercises. Als er bijv. 5 in de combobox staat, worden hier 5 exercises gemaakt

                using (UNET_Service.Service1Client service = new UNET_Service.Service1Client())
                {
                    service.Open();

                    service.SetExerciseCount(Convert.ToInt16(cbxExercise.Text));

                    service.Close();
                }

                GetUNETStatus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, String.Format("Error using WCF methods>{0}", ex.Message));
                log.Error("Error using WCF method change exercise", ex);
                // throw;
            }
        }

        private void cbxRole_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void cbxRadios_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                AddToListbox(string.Format("Set Radios to: {0}", Convert.ToInt16(cbxRadios.SelectedValue)), Color.LimeGreen);
                // we mocken hier een aantal radios. Als er bijv. 5 in de combobox staat, worden hier 5 radios gemaakt

                using (UNET_Service.Service1Client service = new UNET_Service.Service1Client())
                {
                    service.Open();

                    service.SetRadiosCount(Convert.ToInt16(cbxRadios.Text));

                    service.Close();
                }

                GetUNETStatus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, String.Format("Error using WCF methods>{0}", ex.Message));
                log.Error("Error using WCF method change role", ex);
                // throw;
            }
        }

        private void cbxTrainee_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                AddToListbox(string.Format("Set Trainees to: {0}", Convert.ToInt16(cbxTrainee.SelectedValue)), Color.LimeGreen);
                // we mocken hier een aantal radios. Als er bijv. 5 in de combobox staat, worden hier 5 radios gemaakt

                using (UNET_Service.Service1Client service = new UNET_Service.Service1Client())
                {
                    service.Open();

                    service.SetTraineesCount(Convert.ToInt16(cbxTrainee.Text));

                    service.Close();
                }

                GetUNETStatus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, String.Format("Error using WCF methods>{0}", ex.Message));
                log.Error("Error using WCF method change trainee", ex);
                // throw;
            }
        }

        private void cbxRole_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                AddToListbox(string.Format("Set Roles to: {0}", Convert.ToInt16(cbxRole.SelectedValue)), Color.LimeGreen);
                // we mocken hier een aantal roles. Als er bijv. 5 in de combobox staat, worden hier 5 roles gemaakt

                using (UNET_Service.Service1Client service = new UNET_Service.Service1Client())
                {
                    if (service.State != System.ServiceModel.CommunicationState.Opened)
                    {
                        service.Open();
                    }
                    service.SetRolesCount(Convert.ToInt16(cbxRole.Text));

                    service.Close();
                }

                GetUNETStatus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, String.Format("Error using WCF methods>{0}", ex.Message));
                log.Error("Error using WCF method change role", ex);
                // throw;
            }
        }

        /// <summary>
        /// at program start of after a clearing of the listbox, you want to see what the current server status is
        /// </summary>
        private void GetServerStatus()
        {

        }

        /// <summary>
        /// Get the latest status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                // we mocken hier een aantal exercises. Als er bijv. 5 in de combobox staat, worden hier 5 exercises gemaakt
                //    using (UNET_Service.Service1Client service = new UNET_Service.Service1Client())
                //    {
                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }


                //The radiobuttons only have to be refreshed when there actually is something changed, hence the traineestatuschanged bool
                if (service.GetTraineeStatusChanged() == true)
                {

                    //enable the Exercise buttons
                    bool[] traineestatus = service.GetTraineeStatus();
                    if (traineestatus != null)
                    {
                        for (int i = 0; i <= 7; i++)
                        {

                            AddToListbox(string.Format("Trainee status: Trainee{0}: {1}", i, Convert.ToString(traineestatus[i])));
                        }
                    }
                    //          }

                }


                //if something is changed with the noiselevels, update the listbox
                if (service.GetNoiseLevelChanged() == true)
                {

                    lbxNoiseLevel.Items.Clear();

                    //enable the Exercise buttons
                    List<Radio> radiolist = service.GetRadios().ToList<Radio>();
                    int i = 0;
                    foreach (Radio radio in radiolist)
                    {
                        lbxNoiseLevel.Items.Add(string.Format("Radio {0} Noise: {1}", i, Convert.ToString(radio.NoiseLevel)));
                        i++;
                    }
                }
            }

            catch (Exception ex)
            {
                log.Error("Error updating screen controls", ex);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listBoxGetmethods.Items.Clear();

            GetUNETStatus();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmUNETTester_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            service.Close();
        }
    }
}
