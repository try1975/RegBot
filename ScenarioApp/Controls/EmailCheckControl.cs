using Common.Service;
using PuppeteerService;
using ScenarioApp.Controls.Interfaces;
using ScenarioApp.Ninject;
using ScenarioService;
using System;
using System.Windows.Forms;

namespace ScenarioApp.Controls
{
    public partial class EmailCheckControl : UserControl, IEmailCheckControl
    {
        public EmailCheckControl()
        {
            InitializeComponent();
            button1.Click += button1_Click;
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Utils.SaveLinesToFile(textBox2.Lines);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            var progressLog = new Progress<string>(update => textBox3.AppendText(update + Environment.NewLine));
            var engine = new EmailCheck(chromiumSettings: CompositionRoot.Resolve<IChromiumSettings>(), progressLog: progressLog);
            var result = await engine.RunScenario(emails: textBox1.Lines);
            textBox2.Lines = result.ToArray();
        }
    }
}
