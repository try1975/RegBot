using System;
using System.Windows.Forms;
using log4net;
using ScenarioApp;
using ScenarioApp.Ninject;

namespace Topol.UseApi.Forms
{
    public partial class AuthenticationForm : Form
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AuthenticationForm));
        private readonly string Login = "SpecialBrowser";
        private readonly string Password = "12345";
        public AuthenticationForm()
        {
            InitializeComponent();
            tbLogin.Text = Login;
            tbPassword.Text = Password;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (!tbLogin.Text.Equals(Login) || !tbPassword.Text.Equals(Password))
            {
                MessageBox.Show(@"Please enter login and password.");
                return;
            }
            var mainForm = CompositionRoot.Resolve<ScenarioMain>();
            Hide();
            Log.Debug("Start mainform");
            mainForm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}