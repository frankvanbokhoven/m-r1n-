using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StubSIM2UNET
{
    public partial class FrmMainStub : Form
    {
        private PcapPlayer pcapPlayer;
        public FrmMainStub()
        {
            InitializeComponent();
        }

        private void btnSelectLogDir_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();

            theDialog.Title = "Select a PCAP file";
            theDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            theDialog.Filter = "PCAP files|*.pcap";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                tbxLog4NetDirectory.Text = theDialog.FileName;
            }

            //now, ingest the PCAP file
            //   if(pcapPlayer != null)
            ////   {
            //       pcapPlayer.
            //   }
            if (theDialog.FileName.Length == 0)
            {
                MessageBox.Show("Select a PCAP file first");
            }
            else
            {
                pcapPlayer = new PcapPlayer(tbxDestination.Text.Trim(), theDialog.FileName);
                tbPCap.Maximum = pcapPlayer.Count;
                tbPCap.Minimum = 0;
                tbPCap.Value = 10;

                toolStripProgressBar1.Maximum = pcapPlayer.Count;
                
                lbxPCAP.Items.Clear();
                lbxPCAP.Items.AddRange(pcapPlayer.Lines.ToArray());
              //  foreach(String line in pcapPlayer.Lines )
            }


        }

        private void FrmMainStub_Load(object sender, EventArgs e)
        {

        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (btnPlay.ImageIndex == 0)
            {
                btnPlay.ImageIndex = 1;
            }
            else
            {
                btnPlay.ImageIndex = 0;
            }
        }

        private void tbPCap_Scroll(object sender, EventArgs e)
        {
           if(tbPCap.Value <= lbxPCAP.Items.Count)
            {
                lbxPCAP.SelectedIndex = tbPCap.Value;
                toolStripStatusLabel1.Text = "Regel: " + tbPCap.Value;
                toolStripProgressBar1.Value = tbPCap.Value;
            }
        }

        private void lbxPCAP_Click(object sender, EventArgs e)
        {
            ///occurs when the user selects a record in the listbox
            if(lbxPCAP.Items.Count >= tbPCap.Value)
            {
                tbPCap.Value = lbxPCAP.SelectedIndex;
                toolStripStatusLabel1.Text = "Regel: " + tbPCap.Value;
                toolStripProgressBar1.Value = tbPCap.Value;

            }
        }

        private void cbxUsePCAPTime_CheckedChanged(object sender, EventArgs e)
        {
            cbxInterval.Checked = (cbxUsePCAPTime.Checked == false);
            cmbxInterval.Enabled = cbxInterval.Checked;
        }

        private void cbxInterval_CheckedChanged(object sender, EventArgs e)
        {
            cbxUsePCAPTime.Checked =(cbxInterval.Checked == false);
            cmbxInterval.Enabled = cbxInterval.Checked;
        }
    }
}
