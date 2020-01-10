using log4net;
using log4net.Config;
using ScenarioApp.Ninject;
using System;
using System.Windows.Forms;
using Topol.UseApi.Forms;

[assembly: XmlConfigurator(Watch = true)]

namespace ScenarioApp
{
    static class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CompositionRoot.Wire(new ApplicationModule());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AuthenticationForm());
        }
    }
}
