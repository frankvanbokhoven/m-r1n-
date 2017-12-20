using StubSIM2UNET.EasyPCap;
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
       // private PcapPlayer pcapPlayer;
        private int Count = 0;
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



                if (theDialog.FileName.Length == 0)
                {
                    MessageBox.Show("Select a PCAP file first");
                }
                else
                {
                    Count = 0;
                    try
                    {
                        lbxPCAP.Items.Clear();
                        this.Cursor = Cursors.WaitCursor;
                        PcapFile dump = new PcapFile(theDialog.FileName);
                        PcapPacket packet = null;
                        while ((packet = dump.ReadPacket()) != null)
                        {
                            //   Console.WriteLine("{0}.{1}: Packet is {2} bytes", packet.Seconds, packet.Microseconds, packet.Data.Length);
                            Count++;
                         string tempstring = (string.Format("Sec: {0} Micr: {1} Length: {2} Data: {3}", packet.Seconds, packet.Microseconds, packet.Data.Length, System.Text.Encoding.Default.GetString(packet.Data)));
                            lbxPCAP.Items.Add(tempstring);
                        }

                        //   pcapPlayer = new PcapPlayer(tbxDestination.Text.Trim(), theDialog.FileName);
                        tbPCap.Enabled = Count > 0;
                        tbPCap.Maximum = Count;
                        tbPCap.Minimum = 0;
                        tbPCap.Value = 10;

                        toolStripProgressBar1.Maximum = Count;

                        //  foreach(String line in pcapPlayer.Lines )
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }

        }

        private void FrmMainStub_Load(object sender, EventArgs e)
        {
            tbPCap.Enabled = false;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (btnPlay.ImageIndex == 0)
            {
                btnPlay.ImageIndex = 1;
              //  pcapPlayer.Play();
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

        /// <summary>
        /// when user doubleclicks a record in the listbox, he/she wants to send the record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbxPCAP_DoubleClick(object sender, EventArgs e)
        {
         //   pcapPlayer.
        }
    }
}
