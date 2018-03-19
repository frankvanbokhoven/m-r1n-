﻿using System;
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
using System.Security.Cryptography;

namespace UNET_Tester
{
    public partial class frmUNETTester_Main : Form
    {
        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //wcf service
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

            RefillTraineeAssists();

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
                tbxTraineeIDs.Text = idlist.Substring(0, idlist.Length - 1);

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
                tbxInstructorIDs.Text = idinstructorlist.Substring(0, idinstructorlist.Length - 1);// cbxInstructor.Text = lstInstructor.Count.ToString();


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

              //todo  service.SetExerciseCount(cbxExercise.Text);

                List<Exercise> elist = new List<Exercise>();
                for (int i = 1; i <= Convert.ToInt16(cbxExercise.Text); i++)
                {
                    Exercise exe = new Exercise();
                    exe.Number = i;
                    exe.SpecificationName = txtSpecification.Text + i.ToString("00");
                    exe.ExerciseName = txtName.Text + i.ToString("00");
                    //    exe.TraineesAssigned.Add(new Trainee(1010, "Trainee-1010"));

                    if(cbxAssignTrainees.Checked && i == 1) //only add them to the first exercise if the checkbox is checked
                    {
                        //Trainee
                        var traineelist = service.GetTrainees();
                        List<Trainee> lsttrainee = traineelist.ToList<Trainee>();

                         foreach (Trainee trainee in lsttrainee) //add these trainees to the first exercise
                        {
                            exe.TraineesAssigned.Add(trainee);
                            AddToListbox(string.Format("Trainee: {0}, Name: {1}, added to first exercise", trainee.ID, trainee.Name));
                           
                        }

                    }
                    var radioslist = service.GetRadios();
                    List<Radio> lstRadio = radioslist.ToList<Radio>();
                    foreach(Radio radio in lstRadio)
                    {
                        exe.RadiosAssigned.Add(radio);
                        AddToListbox(string.Format("Radio: {0}, Name: {1}, SE: {2} added to first exercise", radio.ID, radio.Description, radio.SpecialEffect));

                    }

                    var rolesslist = service.GetRoles();
                    List<Role> lstrole = rolesslist.ToList<Role>();
                    foreach (Role rol in lstrole)
                    {
                        exe.RolesAssigned.Add(rol);
                        AddToListbox(string.Format("Rol: {0}, Name: {1} added to first exercise", rol.ID, rol.Name));

                    }


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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, String.Format("Error using WCF methods>{0}", ex.Message));
                log.Error("Error using WCF method change role", ex);
                // throw;
            }
        }


        /// <summary>
        /// voeg de rollen en mogelijk platforms toe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                List<Platform> lstPlatforms = new List<Platform>();
                if (cbxAddPlatformToRole.Checked)
                {
                    var platformlist = service.GetPlatforms();
                     lstPlatforms = platformlist.ToList<Platform>();

                }
                ///pick a random role description
                string[] Roles = { "BrugOff", "Keuken", "Kapitein", "Matroos", "Kannonnier", "De Rus" };


                List<Role> elist = new List<Role>();
                for (int i = 1; i <= Convert.ToInt16(cbxRole.Text); i++)
                {
                    string role = Roles[new Random().Next(0, Roles.Length)];
                    Role exe = new Role();
                    exe.ID = i;
                    exe.Name = role;

                    if(cbxAddPlatformToRole.Checked)
                    {
                       exe.PlatformsAssigned.AddRange(lstPlatforms);

                    }
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
                    if (cbxKeepAlive.Checked)
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
            if (service.State != System.ServiceModel.CommunicationState.Opened)
            {
                service.Open();
            }

            listBoxGetmethods.Items.Clear();//lijstje leegmaken

            //Voeg voor iedere trainee-id een trainee object toe
            string[] instructorids = tbxInstructorIDs.Text.Split(',');

            List<Instructor> instructorlist = new List<Instructor>();
            int k = 0;
            foreach (Instructor instructor in instructorlist)
            {
                Instructor inst = new Instructor((instructorids[k]), "Stub Instructor: " + k);// let op!! alleen de eerste instructor komt aan bod!!

                // maak een exercise aan voor het aantal in de combobox en voeg deze toe aan de instructor
                List<Exercise> elist = new List<Exercise>();
                for (int i = 1; i <= Convert.ToInt16(cbxExercise.Text); i++)
                {
                    Exercise exe = new Exercise();
                    exe.Number = i;
                    exe.SpecificationName = txtSpecification.Text + i.ToString("00");
                    exe.ExerciseName = txtName.Text + i.ToString("00");

                    if (cbxAssignTrainees.Checked && i == 1) //only add them to the first exercise if the checkbox is checked
                    {
                        //Trainee
                        var traineelist = service.GetTrainees();
                        List<Trainee> lsttrainee = traineelist.ToList<Trainee>();

                        foreach (Trainee trainee in lsttrainee) //add these trainees to the first exercise
                        {
                            exe.TraineesAssigned.Add(trainee);
                            AddToListbox(string.Format("Trainee: {0}, Name: {1}, added to first exercise", trainee.ID, trainee.Name));
                        }

                    }
                    //  elist.Add(exe);
                    inst.Exercises.Add(new Exercise(1, "Exercise test 1"));
                 AddToListbox(string.Format("Added instructor: {0}", 1012));
    }


                instructorlist.Add(inst);
                service.SetInstructors(instructorlist.ToArray());
                k++;
            }
            comboBox1_SelectedValueChanged(sender, e);
            btnRefreshPlatform_Click(sender, e);

            cbxInstructor_SelectedValueChanged(sender, e);
            cbxRadios_SelectedValueChanged(sender, e);
            cbxRole_SelectedValueChanged(sender, e);
            btnRefreshInstructors_Click(sender, e);
            btnRefreshTrainees_Click(sender, e);

            RefillTraineeAssists();
            //add existing radios to the exercise
            if (cbxAssignRadiosToExercise.Checked)
            {
                for (int i = 1; i <= Convert.ToInt16(cbxRadios.Text); i++)
                {
                    service.SetRadioAssignedStatus(instructorids[0], 1, i, true);
                }
            }


        }

        /// <summary>
        /// fill this dropdown with all users: trainees and instructors
        /// </summary>
        private void RefillTraineeAssists()
        {
            cbxAssistTrainee.Items.Clear();
            //trainees
            var resultlisttrainees = service.GetTrainees();
            List<UNET_Classes.Trainee> lstTrainee = resultlisttrainees.ToList<UNET_Classes.Trainee>(); //C# v3 manier om een array in een list te krijgen
            foreach (UNET_Classes.Trainee trainee in lstTrainee) //then ENABLE them, based on whatever comes from the service
            {

                ComboboxItem item = new ComboboxItem();
                item.Text = trainee.ID + "|" + trainee.Name;
                item.Value = trainee.ID;

                comboBox1.Items.Add(item);
                cbxAssistTrainee.Items.Add(item);
            }

            //instructors
            var resultlistinstructors = service.GetInstructors();
            List<UNET_Classes.Instructor> lstInstructor = resultlistinstructors.ToList<UNET_Classes.Instructor>();
            foreach(UNET_Classes.Instructor instr in lstInstructor)
            {
                ComboboxItem itm = new ComboboxItem();
                itm.Text = instr.ID + "|" + instr.Name;
                itm.Value = instr.ID;
                cbxAssistTrainee.Items.Add(itm);

            }
            


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

                 //maak nu een lijstje met trainees, op basis van de id's en  stuur dat naar de service
                Trainee[] listTrainee;// = new List<UNET_Classes.Trainee>();
                List<Trainee> objlist = new List<Trainee>();
                foreach (string tr in traineeids)
                {
                    UNET_Classes.Trainee trainee = new UNET_Classes.Trainee(tr, tr + "_trainee");

                    if (cbxAssignRolesToTrainees.Checked)
                    {
                        trainee.Roles.Clear();//eerst leeg gooien
                        for (int i = 1; i <= Convert.ToInt16(cbxRole.Text); i++)
                        {
                            Role exe = new Role();
                            exe.ID = i;
                            exe.Name = "Testrole-" + i.ToString("00") + "Trainee: " + tr;
                            AddToListbox("Added role to trainee: " + tr);
                            trainee.Roles.Add(exe);
                        }
                    }

              
                  
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
                bool first = true;
                foreach (string inst in instructorids)
                {

                    UNET_Classes.Instructor instructor = new UNET_Classes.Instructor((inst), inst + "_instructor");
                    objlist.Add(instructor);

                    if (cbxAssignRoles.Checked)
                    {
                        //if the loop is in the first run, add the roles to the first instructor (hopefully the only one) as specified in 2.4.3
                        if (first)
                        {
                            AddToListbox(string.Format("Adding roles to instructor: {0}", inst, Color.LimeGreen));
                            // we mocken hier een aantal roles. Als er bijv. 5 in de combobox staat, worden hier 5 roles gemaakt
                            Application.DoEvents();
                            //enable the Roles buttons
                            var rolelist = service.GetRoles();
                            List<UNET_Classes.Role> lstrole = rolelist.ToList<UNET_Classes.Role>(); //C# v3 manier om een array in een list te krijgen
                            foreach (UNET_Classes.Role role in lstrole)
                            {
                                instructor.AssignedRoles.Add(role);
                               
                            }

                            first = false;
                        }
                    }
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


        /// <summary>
        /// request an assist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxAssistTrainee.SelectedIndex >= 0)
                {
                    // we mocken hier een aantal radios. Als er bijv. 5 in de combobox staat, worden hier 5 radios gemaakt

                    if (service.State != System.ServiceModel.CommunicationState.Opened)
                    {
                        service.Open();
                    }

                    string[] splitstr = cbxAssistTrainee.Text.Split('|');

                    service.CreateAssist(splitstr[0].Trim(), splitstr[1].Trim());

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, String.Format("Error using WCF method createassist>{0}", ex.Message));
                log.Error("Error using WCF method change role", ex);
                // throw;
            }
        }

        private void cbxAssistTrainee_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
      
           

                //  if (MessageBox.Show("Are you sure? This 'destroys' all data in the UNET_Service!!", "Really reset?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,MessageBoxOptions.DefaultDesktopOnly,string.Empty) == DialogResult.Yes)
                if(MessageBox.Show("Are you sure? this clears all data from unet_service!", "Really reset?",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                  if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();

                }       service.Reset();
                    listBoxGetmethods.Items.Clear();
                    AddToListbox("Everything is reset");

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, String.Format("Error resetting UNET_Service {0}", ex.Message));
                log.Error("Error resetting UNET_Service", ex);
            }
        }

        private void btnAddPTT_Click(object sender, EventArgs e)
        {
            try
            {

                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }

                string[] splitstr = cbxAssistTrainee.Text.Split('|');

                service.AddPTT(splitstr[0], splitstr[1].ToLower().Contains("instr") ? UNET_Service.PTTuser.puInstructor : UNET_Service.PTTuser.puTrainee);
      

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, String.Format("Error resetting UNET_Service {0}", ex.Message));
                log.Error("Error resetting UNET_Service", ex);
            }

        }

        private void btnAcknowledgePTT_Click(object sender, EventArgs e)
        {
            try
            {

                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }
                string[] splitstr = cbxAssistTrainee.Text.Split('|');

                service.AcknowledgePTT(splitstr[0]);
      
      

                service.AcknowledgePTT(splitstr[0]);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, String.Format("Error resetting UNET_Service {0}", ex.Message));
                log.Error("Error resetting UNET_Service", ex);
            }
        }

        private void btnRefreshPlatform_Click(object sender, EventArgs e)
        {
            try
            {
                //Voeg voor iedere plaforms to
                string[] platforms = tbxPlatform.Text.Split(',');
                // we mocken hier een aantal radios. Als er bijv. 5 in de combobox staat, worden hier 5 radios gemaakt

                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }

                //maak nu een lijstje met trainees, op basis van de id's en  stuur dat naar de service
                Platform[] listPlatform;
                List<Platform> objlist = new List<Platform>();
                foreach (string plat in platforms)
                {
                    UNET_Classes.Platform platform = new UNET_Classes.Platform(plat);
                    AddToListbox("Set Platform: " + plat, Color.LimeGreen);
                    
                 
                    objlist.Add(platform);
                }
                listPlatform = (Platform[])objlist.ToArray();

                service.SetPlatforms((Platform[])listPlatform);
      
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, String.Format("Error using WCF methods>{0}", ex.Message));
                log.Error("Error using WCF method adding platforms", ex);
                // throw;
            }
        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
