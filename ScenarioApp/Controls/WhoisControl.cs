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
            button4.Click += button4_Click;
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Utils.SaveLinesToFile(textBox7.Lines);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            textBox7.Clear();
            var progress = new Progress<string>(update => textBox7.AppendText(update + Environment.NewLine));
            var domainCheck = new NicRuWhois(chromiumSettings: CompositionRoot.Resolve<IChromiumSettings>(), progressLog: progress);
            await domainCheck.RunScenario(domains: textBox8.Lines);
        }
    }
}
