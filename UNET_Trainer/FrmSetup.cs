using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UNET_Trainer
{
    public partial class FrmSetup : FrmUNETbaseSub
    {
        public FrmSetup()
        {
            InitializeComponent();
            FormTitle = "Setup";


            //basic fill of drowdownlists
            PopulateDropDownColor();
            FillFonts();
        }


        public void PopulateDropDownColor()
        {
            // Make an instance of Color
            System.Drawing.Color c1 = new System.Drawing.Color();
            // Get the type of instance
            Type t = c1.GetType();
            foreach (PropertyInfo p1 in t.GetProperties())
            {
                ColorConverter d = new ColorConverter();
                try
                {
                    // Add Items in DropDownList
                    ddlColorButton.Items.Add(p1.Name);
                }
                catch
                {
                    // Catch exceptions here
                }
            }
        }

        public void FillFonts()
        {
            foreach (FontFamily oneFontFamily in FontFamily.Families)
            {
               ddlFont.Items.Add(oneFontFamily.Name);
            }

            ddlFont.Text = "";
        }

        private void ddlColorButton_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pnlColorExample.BackColor = Color.FromName(ddlColorButton.SelectedItem.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error changing index: " + ex.Message);
            }
        }

        private void ddlFont_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    FontFamily newfont = new FontFamily(ddlFont.SelectedItem.ToString());
            lblTestFont.Font = new Font(ddlFont.SelectedItem.ToString(), lblTestFont.Font.Size);
        }
    }
}
