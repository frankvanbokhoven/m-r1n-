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
using UNET_Classes;
using UNET_Theming;

namespace UNET_Trainer
{
    public partial class FrmRoles : Form
    {
        //log4net
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [DllImport("user32.dll")]
        protected static extern IntPtr GetForegroundWindow();


        private int SelectedExercise;
        private int InstructorID = -1;
        private Instructor CurrentInstructor;


        private UNET_Service.Service1Client service = new UNET_Service.Service1Client();
        public FrmRoles()
        {
            InitializeComponent();
            pnlRoles.Paint += UC_Paint;
        }


        public FrmRoles(int _exersise, int _instructorID)
        {
            SelectedExercise = _exersise;
            InstructorID = _instructorID;
            //we moeten  de huidige status ophalen van de instructeur/exercises/trainee/roles/radios
             //en hiermee de knoppen de juiste kleur geven
     
            
            InitializeComponent();

            pnlRoles.Paint += UC_Paint;
        }



        /// <summary>
        /// Zorg dat de panels een witte border krijgen als ze een dargray opvulkleur hebben
        /// https://stackoverflow.com/questions/76455/how-do-you-change-the-color-of-the-border-on-a-group-box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UC_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.White, ButtonBorderStyle.Solid);
 
        }

        private void FrmRoles_Load(object sender, EventArgs e)
        {
            lblRoleTitle.Text = "Role assignment    Selected excercise: " + SelectedExercise + "   Instructor: " + InstructorID;
        

           timer1.Enabled = true;

            Theming the = new Theming();
            the.SetTheme(UNET_Classes.UNETTheme.utDark, this);
            the.InitPanels(pnlRoles);

            the.SetFormSizeAndPosition(this);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (GetForegroundWindow() == this.Handle)
            {
                SetButtonStatus(this);
            }
        }

        private void FrmRoles_FormClosing(object sender, FormClosingEventArgs e)
        {
            service.Close();

        }

        /// <summary>
        /// This routine sets the statusled of each button, depending on its status
        /// It also enables/disables buttons based on the number of exercises given bij the service
        /// </summary>
        private void SetButtonStatus(Control parent)
        {
            try
            {
                CurrentInstructor = service.GetAllInstructorData(InstructorID);
                // we ask the WCF service (UNET_service) what exercises there are and display them on the screen by making buttons
                // visible/invisible and also set the statusled
                //  {
                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }

                var rolelist = service.GetRoles();
                List<UNET_Classes.Role> lstrole = rolelist.ToList<UNET_Classes.Role>(); //C# v3 manier om een array in een list te krijgen
                foreach (Control ctrl in pnlRoles.Controls)
                {
                    if (ctrl.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        ctrl.Enabled = false;
                    }
                }

                //now we make visible a button for every existing role
                foreach (UNET_Classes.Role role in lstrole)
                {
                    pnlRoles.Controls["btnRole" + role.ID.ToString("00")].Text = string.Format("Role {0}{1}{2}", role.ID, Environment.NewLine, role.Name);

                    pnlRoles.Controls["btnRole" + role.ID.ToString("00")].Enabled = true;
                    pnlRoles.Controls["btnRole" + role.ID.ToString("00")].BackColor = Theming.RoleNotSelectedButton;

                }
                //loop nu door de lijst van toegewezen roles heen en kijk of er een is die aan deze instructor/exercise is toegewezen. 
                //zoja, vul de informatie in en enable de knop met de role-toegewezen-kleur
                if (InstructorID != -1)
                {
                    if (!Object.ReferenceEquals(CurrentInstructor, null))
                    {
                        if (!Object.ReferenceEquals(CurrentInstructor.Exercises, null))
                        {
                            if (SelectedExercise != -1)
                            {
                                foreach (Role assignedRole in CurrentInstructor.Exercises.FirstOrDefault(x => x.Number == SelectedExercise).RolesAssigned) //pak van de bij exercises geselecteerde exercise, de lijst van toegewezen trainees en gebruik die om de buttons te kleuren
                                {
                                    //    if (assignedRole.ID == role.ID)
                                    //    {
                                    //   pnlRoles.Controls["btnRole" + role.ID.ToString("00")].Enabled = true;
                                    pnlRoles.Controls["btnRole" + assignedRole.ID.ToString("00")].BackColor = Theming.RoleSelectedButton;
                                    pnlRoles.Controls["btnRole" + assignedRole.ID.ToString("00")].Text += string.Format("{0}Instructor: {1}", Environment.NewLine, CurrentInstructor.ID + " " + CurrentInstructor.Name);


                                    //    }
                                }
                            }
                        }
                    }
                }


                //now resize all buttons to make optimal use of the available room
                UNET_Classes.Helpers.ResizeButtons(pnlRoles, lstrole.Count, "role");
            }
            catch (Exception ex)
            {
                log.Error("Error using WCF SetButtonStatus", ex);
                Console.Write("Error SetButtonStatus: " + ex.Message);
                // throw;
            }
        }

     
        private void btnMainPage_Click(object sender, EventArgs e)
        {
            //  FrmUNETMain frm = new FrmUNETMain();
            //      frm.Show();
            // based on:  http://stackoverflow.com/questions/1403600/how-to-avoid-multiple-instances-of-windows-form-in-c-sharp
            FrmUNETMain.GetForm.Show();
            this.Close();

        }

        private void btnRole01_Click(object sender, EventArgs e)
        {
            try
            {
                //    SetStatusAndColorRoleButtons((Button)sender);



                string name = ((Button)sender).Text.Substring(0, ((Button)sender).Text.IndexOf("\r\n"));

                string[] splitstring = name.Split(' ');
                int roleIndex = Convert.ToInt16(splitstring[1].ToString());


                if (service.State != System.ServiceModel.CommunicationState.Opened)
                {
                    service.Open();
                }

                //voeg de trainee toe (of verwijder hem juist) aan de lijst van toegewezen trainees per exercise
                if(((Button)sender).BackColor == Theming.RoleSelectedButton)
                  service.SetRoleAssignedStatus(InstructorID, SelectedExercise, roleIndex, false);
                else
                    service.SetRoleAssignedStatus(InstructorID, SelectedExercise, roleIndex, true);

            }
            catch (Exception ex)
            {
                log.Error("Error setting role", ex);
                Console.Write("Error setting role: " + ex.Message);
                // throw;
            }
        }
    }
}
