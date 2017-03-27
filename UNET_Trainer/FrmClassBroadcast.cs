using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UNET_Trainer
{
    public partial class FrmClassBroadcast : FrmUNETbaseSub
    {
        public FrmClassBroadcast()
        {
            InitializeComponent();
        }

        private void FrmClassBroadcast_Load(object sender, EventArgs e)
        {
            FormTitle = "Class broadcast";

            InitState();
        }


        /// <summary>
        /// Init this screen as described in 2.2.1
        /// </summary>
        private void InitState()
        {
            //loop thrue the Monitortraineearray to set the proper status
          //  for (int i = 0; i <= 15; i++)
          //  {
          //      MonitorTraineeArray[i] = false;
          //  }

            // A little amateur.. but it just is the fastest manner
            btnTraineeAA.BackColor = Constants.Extinguised;
            btnTraineeBB.BackColor = Constants.Extinguised;
            btnTraineeCC.BackColor = Constants.Extinguised;
            btnTraineeDD.BackColor = Constants.Extinguised;
            btnTraineeEE.BackColor = Constants.Extinguised;
            btnTraineeFF.BackColor = Constants.Extinguised;
            btnTraineeGG.BackColor = Constants.Extinguised;
            btnTraineeHH.BackColor = Constants.Extinguised;
            btnTraineeJJ.BackColor = Constants.Extinguised;
            btnTraineeKK.BackColor = Constants.Extinguised;
            btnTraineeLL.BackColor = Constants.Extinguised;
            btnTraineeMM.BackColor = Constants.Extinguised;
            btnTraineeNN.BackColor = Constants.Extinguised;
            btnTraineePP.BackColor = Constants.Extinguised;
            btnTraineeRR.BackColor = Constants.Extinguised;
            btnTraineeSS.BackColor = Constants.Extinguised;

            btnBroadcast.BackColor = Constants.Extinguised;
            btnSelectAllInstructors.BackColor = Constants.Extinguised;
            btnSelectAllPositions.BackColor = Constants.Extinguised;
            btnSelectAllTrainees.BackColor = Constants.Extinguised;

            btnInstructor01.BackColor = Constants.Extinguised;
            btnInstructor02.BackColor = Constants.Extinguised;
            btnInstructor03.BackColor = Constants.Extinguised;
            btnInstructor04.BackColor = Constants.Extinguised;
        }
    }
}
