using System;
using System.Windows.Forms;
using AutoMapper;
using AutoMapper.Configuration;
using log4net;
using log4net.Config;
using RegBot.Demo.Ninject;


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
                CompositionRoot.Wire(new ApplicationModule());
                var cfg = new MapperConfigurationExpression();
                AutoMapperConfig.RegisterMappings(cfg);
                Mapper.Initialize(cfg);
                
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
