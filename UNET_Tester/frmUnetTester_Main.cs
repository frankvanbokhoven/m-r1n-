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
using UNET_Classes;

namespace UNET_Tester
{
    public partial class frmUNETTester_Main : Form
    {
        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private UNET_Service.Service1Client service = new UNET_Service.Service1Client();
        private int KeepAliveCounter = 0;

        public frmUNETTester_Main()
        {
            InitializeComponent();
            log4net.Config.BasicConfigurator.Configure();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            cbxExercise.Text = ConfigurationManager.AppSettings["Exersise"];
            cbxRadios.Text = ConfigurationManager.AppSettings["Radio"];
            cbxRole.Text = ConfigurationManager.AppSettings["Role"];
            tbxTraineeIDs.Text = ConfigurationManager.AppSettings["Trainee"];
            tbxInstructorIDs.Text = ConfigurationManager.AppSettings["Instructor"];
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
                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }

                // we mocken hier een aantal exercises. Als er bijv. 5 in de combobox staat, worden hier 5 exercises gemaakt
                //Exercises
                var resultlist = service.GetExercises();
                List<Exercise> lst = resultlist.ToList<Exercise>(); //C# v3 manier om een array in een list te krijgen

                foreach (Exercise exer in lst)
                {
                    AddToListbox(string.Format("Exercise: {0}, Name: {1}, Specificationname: {2}", exer.Number, exer.ExerciseName, exer.SpecificationName));
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

                //bouw een csv lijstje op met de trainees
                string idlist = string.Empty;
                foreach (Trainee trainee in lsttrainee)
                {
                    AddToListbox(string.Format("Trainee: {0}, Name: {1}", trainee.ID, trainee.Name));
                    idlist += trainee.ID + ",";
                }
                tbxTraineeIDs.Text = idlist.Substring(0, idlist.Length-1);

                //Radio
                var radiolist = service.GetRadios();
                List<Radio> lstRadio = radiolist.ToList<Radio>(); //C# v3 manier om een array in een list te krijgen

                foreach (Radio radio in lstRadio)
                {
                    AddToListbox(string.Format("Radio: {0}, Name: {1}", radio.ID, radio.Description));
                }
                cbxRadios.Text = lstRadio.Count.ToString();



                //Instructor
                var instructorlist = service.GetInstructors();
                List<Instructor> lstInstructor = instructorlist.ToList<Instructor>(); //C# v3 manier om een array in een list te krijgen
                string idinstructorlist = string.Empty;
                foreach (Instructor instructor in lstInstructor)
                {
                    AddToListbox(string.Format("Instructor: {0}, Name: {1}", instructor.ID, instructor.Name));
                    idinstructorlist += instructor.ID + ",";
                }
                tbxInstructorIDs.Text  = idinstructorlist.Substring(0, idinstructorlist.Length - 1);// cbxInstructor.Text = lstInstructor.Count.ToString();


                ///updaten noiselevel
                lbxNoiseLevel.Items.Clear();
                int i = 0;
                foreach (Radio radio in radiolist)
                {
                    lbxNoiseLevel.Items.Add(string.Format("Radio {0} Noise: {1} State: {2}", i, Convert.ToString(radio.NoiseLevel), radio.State.ToString()));
                    i++;
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
                AddToListbox(string.Format("Set Exercises to: {0}", Convert.ToInt16(cbxExercise.Text)), Color.LimeGreen);
                // we mocken hier een aantal exercises. Als er bijv. 5 in de combobox staat, worden hier 5 exercises gemaakt

                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }
        
                service.SetExerciseCount(Convert.ToInt16(cbxExercise.Text));

                List<Exercise> elist = new List<Exercise>();
                 for(int i = 1; i<= Convert.ToInt16(cbxExercise.Text); i++)
                {
                   Exercise  exe = new Exercise();
                    exe.Number = i;
                    exe.SpecificationName = txtSpecification.Text + i.ToString("00");
                    exe.ExerciseName = txtName.Text + i.ToString("00");
                    elist.Add(exe);
                }
                //add the list of exercises to the service
                service.SetExercises(elist.ToArray());

 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, String.Format("Error using WCF methods>{0}", ex.Message));
                log.Error("Error using WCF method change exercise", ex);
            }
        }



        private void cbxRadios_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                AddToListbox(string.Format("Set Radios to: {0}", Convert.ToInt16(cbxRadios.Text)), Color.LimeGreen);
                // we mocken hier een aantal radios. Als er bijv. 5 in de combobox staat, worden hier 5 radios gemaakt

                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }
          
                service.SetRadiosCount(Convert.ToInt16(cbxRadios.Text));

           

                //     GetUNETStatus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, String.Format("Error using WCF methods>{0}", ex.Message));
                log.Error("Error using WCF method change role", ex);
                // throw;
            }
        }

        private void cbxRole_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                AddToListbox(string.Format("Set Roles to: {0}", Convert.ToInt16(cbxRole.Text)), Color.LimeGreen);
                // we mocken hier een aantal roles. Als er bijv. 5 in de combobox staat, worden hier 5 roles gemaakt

                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }


                // service.SetRolesCount(Convert.ToInt16(cbxRole.Text));
                List<Role> elist = new List<Role>();
                for (int i = 1; i <= Convert.ToInt16(cbxRole.Text); i++)
                {
                    Role exe = new Role();
                    exe.ID = i;
                    exe.Name = "Testrole-" + i.ToString("00");
                    elist.Add(exe);
                }

                service.SetRoles(elist.ToArray());


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
                    int i = 1;
                    foreach (Radio radio in radiolist)
                    {
                        lbxNoiseLevel.Items.Add(string.Format("Radio {0} Noise: {1}", i, Convert.ToString(radio.NoiseLevel)));
                        i++;
                    }
                }

                //keep alive
                if (KeepAliveCounter == Convert.ToInt16(cbxKeepAlive.Text))
                {
                    if(cbxKeepAlive.Checked)
                        {
                            buttonRefresh_Click(sender, e);//klik eenvoudigweg de button, dan zorgt dat event voor de keepalive
                            KeepAliveCounter = 0;
                        }
                }
                else
                    KeepAliveCounter++;
            }

            catch (Exception ex)
            {
                log.Error("Error updating screen controls", ex);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listBoxGetmethods.Items.Clear();

            //   GetUNETStatus();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmUNETTester_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            service.Close();
        }

        private void cbxInstructor_SelectedValueChanged(object sender, EventArgs e)
        {
          
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            comboBox1_SelectedValueChanged(sender, e);
            cbxInstructor_SelectedValueChanged(sender, e);
            cbxRadios_SelectedValueChanged(sender, e);
            cbxRole_SelectedValueChanged(sender, e);
            btnRefreshInstructors_Click(sender, e);
            btnRefreshTrainees_Click(sender, e);
        }

        private void btnRefreshTrainees_Click(object sender, EventArgs e)
        {
            try
            {
                //Voeg voor iedere trainee-id een trainee object toe
                string[] traineeids = tbxTraineeIDs.Text.Split(',');
                // we mocken hier een aantal radios. Als er bijv. 5 in de combobox staat, worden hier 5 radios gemaakt

                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }

             //   service.SetTraineesCount(Convert.ToInt16(traineeids.Length));

                //maak nu een lijstje meet trainees, op basis van de id's en  stuur dat naar de service
                Trainee[] listTrainee;// = new List<UNET_Classes.Trainee>();
                List<Trainee> objlist = new List<Trainee>();
                foreach(string tr in traineeids)
                {
                    UNET_Classes.Trainee trainee = new UNET_Classes.Trainee(Convert.ToInt16(tr), tr + "_trainee");
                    objlist.Add(trainee);
                }
                listTrainee = (Trainee[])objlist.ToArray();
               
                service.SetTrainees((Trainee[])listTrainee);
                AddToListbox(string.Format("Set Trainees to: {0}", traineeids.Length), Color.LimeGreen);
 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, String.Format("Error using WCF methods>{0}", ex.Message));
                log.Error("Error using WCF method change trainee", ex);
                // throw;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnRefreshInstructors_Click(object sender, EventArgs e)
        {
            try
            {
                AddToListbox(string.Format("Set Instructors to: {0}", tbxInstructorIDs.Text), Color.LimeGreen);
                //Voeg voor iedere trainee-id een trainee object toe
                string[] instructorids = tbxInstructorIDs.Text.Split(',');
                // we mocken hier een aantal radios. Als er bijv. 5 in de combobox staat, worden hier 5 radios gemaakt

                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }

                //maak nu een lijstje met instructors, op basis van de id's en  stuur dat naar de service
                Instructor[] listInstructors;
                List<Instructor> objlist = new List<Instructor>();
                foreach (string inst in instructorids)
                {
                    UNET_Classes.Instructor instructor = new UNET_Classes.Instructor(Convert.ToInt16(inst), inst + "_instructor");
                    objlist.Add(instructor);
                }
                listInstructors = (Instructor[])objlist.ToArray();

                service.SetInstructors((Instructor[])listInstructors);
          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, String.Format("Error using WCF methods>{0}", ex.Message));
                log.Error("Error using WCF method change role", ex);
                // throw;
            }
        }

        private void cbxKeepAlive_CheckedChanged(object sender, EventArgs e)
        {
            KeepAliveCounter = 0;
        }
    }
}
