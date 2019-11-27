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
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            textBox7.Clear();
            var progress = new Progress<string>(update => textBox7.AppendText(update + Environment.NewLine));
            var domainCheck = new NicRuWhois(chromiumSettings: CompositionRoot.Resolve<IChromiumSettings>(), progressLog: progress);
            await domainCheck.RunScenario(domain: textBox8.Text);
        }
    }
}
