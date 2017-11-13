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
        //log4net
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
            ///Haal de settings op uit de registry. Dit mislukt de allereerste keer
            UNET_Classes.UNETTheme theme;
            switch (RegistryAccess.GetStringRegistryValue(@"UNET", @"theme", "dark"))
            {
                case "dark":
                    {
                        theme = UNET_Classes.UNETTheme.utDark;
                        break;
                    }
                case "light":
                    {
                        theme = UNET_Classes.UNETTheme.utLight;
                        break;
                    }
                case "blue":
                    {
                        theme = UNET_Classes.UNETTheme.utBlue;
                        break;
                    }
                default:
                    {
                        theme = UNET_Classes.UNETTheme.utDark;
                        break;
                    }

            }
            Theming the = new Theming();
            the.SetTheme(UNET_Classes.UNETTheme.utDark, this);
            the.SetFormSizeAndPosition(this);

            //pick a font
            ddlFont.SelectedText = RegistryAccess.GetStringRegistryValue(@"UNET", @"font", "Arial Rounded MT");
            Font ft = new Font("Arial Rounded MT", 12);
            lblTestFont.Font = ft;
            //pick a color
            ddlColorButton.SelectedText = RegistryAccess.GetStringRegistryValue(@"UNET", @"colorbutton", "darkgrey");
            btnMainPage.Focus();
            //account
            tbxAccount.Text = RegistryAccess.GetStringRegistryValue(@"UNET", @"account", "1013");
            //displayname
            tbxDisplayName.Text = RegistryAccess.GetStringRegistryValue(@"UNET", @"displayname", "Trainee 1013");
            //sipserver
            txtSipServer.Text = RegistryAccess.GetStringRegistryValue(@"UNET", @"sipserver", "10.0.128.128");
            //account
            txtDomain.Text = RegistryAccess.GetStringRegistryValue(@"UNET", @"domain", "unet");
            //account
            txtPort.Text = RegistryAccess.GetStringRegistryValue(@"UNET", @"port", "5060");

            //log dir
            //string file = ((Hierarchy)LogManager.GetRepository())
            // .Root.Appenders.OfType<FileAppender>().FirstOrDefault().File;
            tbxLog4NetDirectory.Text = RegistryAccess.GetStringRegistryValue(@"UNET", @"log4netdir", @"c:\Marine\Log");
            tbxLogDirectory.Text = RegistryAccess.GetStringRegistryValue(@"UNET", @"logdir", @"c:\Marine\Log");

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
                tbxLog4NetDirectory.Text = theDialog.SelectedPath;
            }

        }

        private void FrmSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            //sla de wijzigingen op naar de app.config van deze trainer
            try
            {
                ///sla de settings op uit in registry. Dit mislukt de allereerste keer
                RegistryAccess.SetStringRegistryValue(@"UNET", @"colorbutton", ddlColorButton.Text.ToString());
                RegistryAccess.SetStringRegistryValue(@"UNET", @"font", ddlFont.Text.ToString());
                RegistryAccess.SetStringRegistryValue(@"UNET", @"logdir", tbxLogDirectory.Text.ToString());
                RegistryAccess.SetStringRegistryValue(@"UNET", @"log4netdir", tbxLog4NetDirectory.Text.ToString());
                RegistryAccess.SetStringRegistryValue(@"UNET", @"account", tbxAccount.Text.ToString());
                RegistryAccess.SetStringRegistryValue(@"UNET", @"displayname", tbxDisplayName.Text.ToString());
                RegistryAccess.SetStringRegistryValue(@"UNET", @"sipserver", txtSipServer.Text.ToString());
                RegistryAccess.SetStringRegistryValue(@"UNET", @"domain", txtDomain.Text.ToString());
                RegistryAccess.SetStringRegistryValue(@"UNET", @"port", txtPort.Text.ToString());
        
                if (rbDark.Checked)
                    RegistryAccess.SetStringRegistryValue(@"UNET", @"theme", "dark");
                if (rbLight.Checked)
                    RegistryAccess.SetStringRegistryValue(@"UNET", @"theme", "light");
                if (rbBlue.Checked)
                    RegistryAccess.SetStringRegistryValue(@"UNET", @"theme", "blue");

            }
            catch (Exception ex)
            {
                log.Error("Error saving to registry: " + ex.Message);
            }

        }

        private void btnApplyColors_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog theDialog = new FolderBrowserDialog();

            theDialog.Description = "Select the directory logging directory";
            theDialog.RootFolder = Environment.SpecialFolder.Desktop;
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                tbxLogDirectory.Text = theDialog.SelectedPath;
            }

        }
    }
}
