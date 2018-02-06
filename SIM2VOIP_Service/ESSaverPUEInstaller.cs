using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace XPUEESSaver
{
    [RunInstaller(true)]
    public partial class ESSaverPUEInstaller : Installer
    {
        private Container components;
        private ServiceInstaller serviceInstaller1;
        private ServiceProcessInstaller serviceProcessInstaller1;

        public ESSaverPUEInstaller()
        {
            InitializeComponent();
            //   De PUE service gebruikt de eventlog. Die móet gestart zijn als deze service gestart wordt (zie onderstaand artikel
            //   http://msdn.microsoft.com/en-us/library/system.serviceprocess.serviceinstaller.servicesdependedon%28v=VS.100%29.aspx
            //  serviceInstaller1.ServicesDependedOn
            //    MServiceInstaller1.ServiceName = "Service1"
         //   serviceInstaller1.ServicesDependedOn = new string[] {"eventlog"}; // todo: even kijken welke naam hier gebruikt moet worden.. "Windows Event Log" };
        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

            //           base.OnAfterInstall(savedState);
            //          using (var serviceController = new ServiceController(this.serviceInstaller1.ServiceName, Environment.MachineName))
            //               serviceController.Start();

        }

        private void serviceProcessInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        //   private void InitializeComponent()
        //  {

        //   }

        #endregion
    }
}