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
using UNET_Theming;
using System.Configuration;
using log4net.Repository.Hierarchy;
using log4net;
using log4net.Appender;

namespace UNET_Trainer_Trainee
{
    public partial class FrmSetup : Form
    {
        public FrmSetup()
        {
    
            InitializeComponent();
            this.Text = "Setup";


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

        private void btnMainPage_Click(object sender, EventArgs e)
        {
            //  FrmUNETMain frm = new FrmUNETMain();
            //      frm.Show();
            // based on:  http://stackoverflow.com/questions/1403600/how-to-avoid-multiple-instances-of-windows-form-in-c-sharp
            // FrmUNETMain.GetForm.Show();
            this.Close();

        }

        private void ddlFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTestFont.Font = new Font(ddlFont.SelectedItem.ToString(), lblTestFont.Font.Size);

        }

        private void ddlColorButton_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pnlColorExample.BackColor = Color.FromName(ddlColorButton.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error changing index: " + ex.Message);
            }
        }

        private void FrmSetup_Load(object sender, EventArgs e)
        {
            Theming the = new Theming();
            the.SetTheme(UNET_Classes.UNETTheme.utDark, this);
            the.SetFormSizeAndPosition(this);

            //pick a font
            ddlFont.SelectedText = "Arial Rounded MT";
            Font ft = new Font("Arial Rounded MT", 12);
            lblTestFont.Font = ft;
            //pick a color
            ddlColorButton.SelectedText = "darkgrey";
            btnMainPage.Focus();

            string file = ((Hierarchy)LogManager.GetRepository())
         .Root.Appenders.OfType<FileAppender>().FirstOrDefault().File;
            txtLogDirectory.Text = file;
            btnMainPage.Focus();
        }

        private void btnSelectLogDir_Click(object sender, EventArgs e)
        {
        //    string dirtobeprocessed = System.Configuration. conf  ini.IniReadValue("appsettings", "XMLDirectoryToBeProcessed");

            FolderBrowserDialog  theDialog = new FolderBrowserDialog();

            theDialog.Description = "Select the Log4Net logging directory";
            theDialog.RootFolder = Environment.SpecialFolder.Desktop;
            if(theDialog.ShowDialog() == DialogResult.OK)
            {
                txtLogDirectory.Text = theDialog.SelectedPath;
            }

        }
    }
}
