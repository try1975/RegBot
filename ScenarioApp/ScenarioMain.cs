using log4net;
using ScenarioApp.Controls.Interfaces;
using System;
using System.Windows.Forms;

namespace ScenarioApp
{
    public partial class ScenarioMain : Form
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ScenarioMain));

        public ScenarioMain(IScenarioControl scenarioControl)
        {
            InitializeComponent();
            var control = (Control)scenarioControl;
            control.Dock = DockStyle.Fill;
            panel1.Controls.Add(control);
            FormClosed += ScenarioMain_FormClosed;
        }

        private void ScenarioMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
