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
    public partial class CheckFbAccountControl : UserControl, ICheckFbAccountControl
    {
        private readonly IAccountDataLoader _accountDataLoader;

        public CheckFbAccountControl(IAccountDataLoader accountDataLoader)
        {
            _accountDataLoader = accountDataLoader;
            InitializeComponent();
            button1.Click += button1_Click;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            var progressResult = new Progress<CheckFbAccountOutput>(checkfbAccountResult => ProgressResultMethod(checkfbAccountResult));
            var progressLog = new Progress<string>(update => ProgressLogMethod(update));
            var scenario = new CheckFbAccount(progressLog: progressLog, progressResult: progressResult, chromiumSettings: CompositionRoot.Resolve<IChromiumSettings>());
            await scenario.RunScenario(accountData: _accountDataLoader.GetFbAccountData().FirstOrDefault(), fbAccountNames: textBox1.Lines);
        }

        private void ProgressResultMethod(CheckFbAccountOutput checkfbAccountResult)
        {
            var available = checkfbAccountResult.Available ? "доступен" : "недоступен";
            textBox2.AppendText($"{checkfbAccountResult.CheckDate} {checkfbAccountResult.AccountName} - {available} - {checkfbAccountResult.AccountUrl}{Environment.NewLine}");
        }

        private void ProgressLogMethod(string update)
        {
            textBox3.AppendText(update + Environment.NewLine);
        }
    }
}
