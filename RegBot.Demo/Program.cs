using System;
using System.Windows.Forms;
using log4net;
using log4net.Config;


[assembly: XmlConfigurator(Watch = true)]

namespace RegBot.Demo
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
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                throw;
            }
        }
    }
}
