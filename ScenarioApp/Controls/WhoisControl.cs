using Common.Service;
using PuppeteerService;
using ScenarioApp.Controls.Interfaces;
using ScenarioApp.Ninject;
using ScenarioService;
using System;
using System.Windows.Forms;

namespace ScenarioApp.Controls
{
    public partial class WhoisControl : UserControl, IWhoisControl
    {
        public WhoisControl()
        {
            InitializeComponent();
            btnExecute.Click += BtnExecute_Click;
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Utils.SaveLinesToFile(tbProgress.Lines);
        }

        private async void BtnExecute_Click(object sender, EventArgs e)
        {
            tbProgress.Clear();
            var progress = new Progress<string>(update => tbProgress.AppendText(update + Environment.NewLine));
            var domainCheck = new NicRuWhois(chromiumSettings: CompositionRoot.Resolve<IChromiumSettings>(), progressLog: progress);
            await domainCheck.RunScenario(domains: textBox8.Lines);
        }
    }
}
