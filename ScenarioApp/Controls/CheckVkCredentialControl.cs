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
    public partial class CheckVkCredentialControl : UserControl, ICheckVkCredentialControl
    {
        public CheckVkCredentialControl()
        {
            InitializeComponent();
            button1.Click += Button1_Click;
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            var progressResult = new Progress<CheckVkCredentialOutput>(checkVkAccountResult => ProgressResultMethod(checkVkAccountResult));
            var progressLog = new Progress<string>(update => ProgressLogMethod(update));
            var scenario = new CheckVkCredential(progressLog: progressLog, progressResult: progressResult, chromiumSettings: CompositionRoot.Resolve<IChromiumSettings>());
            var listCheckVkCredentialInput = new List<CheckVkCredentialInput>(textBox1.Lines.Length);
            foreach (var line in textBox1.Lines)
            {
                var loginPassword = line.Split(' ');
                listCheckVkCredentialInput.Add(new CheckVkCredentialInput { Login = loginPassword[0], Password = loginPassword[1] });
            }
            await scenario.RunScenario(listCheckVkCredentialInput);
        }

        private void ProgressResultMethod(CheckVkCredentialOutput checkVkAccountResult)
        {
            textBox2.AppendText($"{JsonConvert.SerializeObject(checkVkAccountResult)}{Environment.NewLine}");
        }

        private void ProgressLogMethod(string update)
        {
            textBox3.AppendText(update + Environment.NewLine);
        }
    }
}
