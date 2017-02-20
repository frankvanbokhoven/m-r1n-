using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("SIP tester based on Sipek softphone")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("http://sites.google.com/site/sipekvoip/")]
[assembly: AssemblyProduct("SIP Tester")]
[assembly: AssemblyCopyright("Copyright © Marine 2017")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("2f57deb8-48d5-402d-bda0-6a2b57bc8ee4")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
[assembly: AssemblyVersion("0.2017.02.20")]
[assembly: AssemblyFileVersion("0.1.0.0")]
// Let log4net know that it can look for configuration in the default application config file
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
