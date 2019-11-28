using Newtonsoft.Json;
using PuppeteerService;
using ScenarioApp.Controls.Interfaces;
using ScenarioApp.Ninject;
using ScenarioService;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ScenarioApp.Controls
{
    public partial class CheckFbCredentialControl : UserControl, ICheckFbCredentialControl
    {
        public CheckFbCredentialControl()
        {
            InitializeComponent();
            button1.Click += Button1_Click;
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            var progressResult = new Progress<CheckFbCredentialOutput>(checkFbAccountResult => ProgressResultMethod(checkFbAccountResult));
            var progressLog = new Progress<string>(update => ProgressLogMethod(update));
            var scenario = new CheckFbCredential(progressLog: progressLog, progressResult: progressResult, chromiumSettings: CompositionRoot.Resolve<IChromiumSettings>());
            var listCheckFbCredentialInput = new List<CheckFbCredentialInput>(textBox1.Lines.Length);
            foreach (var line in textBox1.Lines)
            {
                var loginPassword = line.Split(' ');
                listCheckFbCredentialInput.Add(new CheckFbCredentialInput { Login = loginPassword[0], Password = loginPassword[1] });
            }
            await scenario.RunScenario(listCheckFbCredentialInput);
        }

        private void ProgressResultMethod(CheckFbCredentialOutput checkFbAccountResult)
        {
            textBox2.AppendText($"{JsonConvert.SerializeObject(checkFbAccountResult)}{Environment.NewLine}");
        }

        private void ProgressLogMethod(string update)
        {
            textBox3.AppendText(update + Environment.NewLine);
        }
    }
}
