using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ScenarioApp.Controls.Interfaces;
using Common.Service;
using ScenarioApp.Ninject;
using PuppeteerService;
using ScenarioService;

namespace ScenarioApp.Controls
{
    public partial class SendMailControl : UserControl, ISendMailControl
    {
        public SendMailControl()
        {
            InitializeComponent();
            button1.Click += Button1_Click;
            btnSave.Click += BtnSave_Click;
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            var progressResult = new Progress<string>(update => ProgressResultMethod(update));
            var progressLog = new Progress<string>(update => ProgressLogMethod(update));
            var scenario = new EmailSend(progressLog: progressLog, progressResult: progressResult, chromiumSettings: CompositionRoot.Resolve<IChromiumSettings>());
            var listEmailAndPassword = new List<LoginPasswordInput>(tbMailAccounts.Lines.Length);
            foreach (var line in tbMailAccounts.Lines)
            {
                var loginPassword = line.Split(null);
                listEmailAndPassword.Add(new LoginPasswordInput { Login = loginPassword[0], Password = loginPassword[1] });
            }
            await scenario.RunScenario(listEmailAndPassword: listEmailAndPassword, to: tbTo.Text, subject: tbSubject.Text, emailText: tbMailText.Lines);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Utils.SaveLinesToFile(textBox2.Lines);
        }

        private void ProgressResultMethod(string update)
        {
            textBox2.AppendText($"{update}{Environment.NewLine}");
        }

        private void ProgressLogMethod(string update)
        {
            textBox3.AppendText(update + Environment.NewLine);
        }
    }
}
