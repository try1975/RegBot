using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace ScenarioApp.Data
{
    public class DataSettings : IDataSettings
    {
        public string GetConnectionString()
        {
            return Path.Combine(Application.StartupPath, ConfigurationManager.AppSettings["DbPath"]);
        }
    }
}
