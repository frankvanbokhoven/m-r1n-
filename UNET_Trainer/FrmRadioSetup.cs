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
    public partial class FrmRadioSetup : FrmUNETbaseSub
    {
        private int NoiseLevel = 1;
        public FrmRadioSetup()
        {
            InitializeComponent();


            SetNoiseLevel(1);
        }


#region NoiseLevel

        private void SetNoiseLevel(int _noiselevel)
        {
            NoiseLevel = _noiselevel;
            switch (NoiseLevel)
                {
                case 0:
                    {
                        btn1.BackColor = Color.White;
                        btn2.BackColor = Color.White;
                        btn3.BackColor = Color.White;
                        btn4.BackColor = Color.White;
                        btn5.BackColor = Color.White;
                        btnOff.BackColor = Color.LightBlue; ;
                        break;
                    }
                case 1:
                    {
                        btn1.BackColor = Color.LightBlue;
                        btn2.BackColor = Color.White;
                        btn3.BackColor = Color.White;
                        btn4.BackColor = Color.White;
                        btn5.BackColor = Color.White;
                        btnOff.BackColor = Color.White;
                        break;
                    }
                case 2:
                    {
                        btn1.BackColor = Color.LightBlue;
                        btn2.BackColor = Color.LightBlue;
                        btn3.BackColor = Color.White;
                        btn4.BackColor = Color.White;
                        btn5.BackColor = Color.White;
                        btnOff.BackColor = Color.White;
                        break;
                    }
                case 3:
                    {
                        btn1.BackColor = Color.LightBlue;
                        btn2.BackColor = Color.LightBlue;
                        btn3.BackColor = Color.LightBlue;
                        btn4.BackColor = Color.White;
                        btn5.BackColor = Color.White;
                        btnOff.BackColor = Color.White;
                        break;
                    }
                case 4:
                    {
                        btn1.BackColor = Color.LightBlue;
                        btn2.BackColor = Color.LightBlue;
                        btn3.BackColor = Color.LightBlue;
                        btn4.BackColor = Color.LightBlue;
                        btn5.BackColor = Color.White;
                        btnOff.BackColor = Color.White;
                        break;
                    }
                case 5:
                    {
                        btn1.BackColor = Color.LightBlue;
                        btn2.BackColor = Color.LightBlue;
                        btn3.BackColor = Color.LightBlue;
                        btn4.BackColor = Color.LightBlue;
                        btn5.BackColor = Color.LightBlue;
                        btnOff.BackColor = Color.White;
                        break;
                    }
            }
        }
        #endregion

        private void btnOff_Click(object sender, EventArgs e)
        {
            SetNoiseLevel(0); //todo: enum van maken
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            SetNoiseLevel(1);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            SetNoiseLevel(2);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            SetNoiseLevel(3);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            SetNoiseLevel(4);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            SetNoiseLevel(5);
        }
    }
}
