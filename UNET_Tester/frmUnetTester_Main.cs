using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UNET_Tester.UNET_Service;

namespace UNET_Tester
{
    public partial class frmUNETTester_Main : Form
    {
        //log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public frmUNETTester_Main()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            cbxExercise.SelectedValue = 5;
            GetUNETStatus();
        }

        private void GetUNETStatus()
        {
            try
            {
                // we mocken hier een aantal exercises. Als er bijv. 5 in de combobox staat, worden hier 5 exercises gemaakt
                using (UNET_Service.Service1Client service = new UNET_Service.Service1Client())
                {
                    //  List<Classes.Exercise> exerciselist =
                    var resultlist = service.GetExercises();
                    List<Exercise> lst = resultlist.ToList<Exercise>();

                 //   for (int i = 0; i <= Convert.ToInt16(lst.Count - 1); i++)
                    foreach (Exercise exer in lst)
                    {
                        listBoxGetmethods.Items.Add(string.Format("Exercise: {0}, Name: {1}", exer.Number, exer.SpecificationName));
                    }
                    service.Close();
                }

            }
            catch (Exception ex)
            {
                listBoxGetmethods.Items.Add(String.Format("Error using WCF methods>{0}", ex.Message));
                log.Error("Error using WCF method change exercise", ex);
                // throw;
            }
            finally
            {

            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                listBoxGetmethods.Items.Add(string.Format("Set Exercises to: {0}", Convert.ToInt16(cbxExercise.SelectedValue)));
                // we mocken hier een aantal exercises. Als er bijv. 5 in de combobox staat, worden hier 5 exercises gemaakt

              //  for(int i = 0; i <= Convert.ToInt16(cbxExercise.Text); i++)
              //  {
              //  }
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
    }
}
