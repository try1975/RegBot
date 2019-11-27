using PuppeteerService;
using ScenarioApp.Controls.Interfaces;
using ScenarioApp.Data;
using ScenarioApp.Ninject;
using ScenarioService;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ScenarioApp.Controls
{
    public partial class CheckVkAccountControl : UserControl, ICheckVkAccountControl
    {
        private readonly IAccountDataLoader _accountDataLoader;

        public CheckVkAccountControl(IAccountDataLoader accountDataLoader)
        {
            _accountDataLoader = accountDataLoader;
            InitializeComponent();
            button1.Click += button1_Click;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            var progressResult = new Progress<CheckVkAccountOutput>(checkVkAccountResult => ProgressResultMethod(checkVkAccountResult));
            var progressLog = new Progress<string>(update => ProgressLogMethod(update));
            var scenario = new CheckVkAccount(progressLog: progressLog, progressResult: progressResult, chromiumSettings: CompositionRoot.Resolve<IChromiumSettings>());
            await scenario.RunScenario(accountData: _accountDataLoader.GetAccountData().FirstOrDefault(), vkAccountNames: textBox1.Lines);
        }

        private void ProgressResultMethod(CheckVkAccountOutput checkVkAccountResult)
        {
            var available = checkVkAccountResult.Available ? "доступен" : "недоступен";
            textBox2.AppendText($"{checkVkAccountResult.CheckDate} {checkVkAccountResult.VkAccountName} - {available} - {checkVkAccountResult.VkAccountUrl}{Environment.NewLine}");
        }

        private void ProgressLogMethod(string update)
        {
            textBox3.AppendText(update + Environment.NewLine);
        }
    }
}
