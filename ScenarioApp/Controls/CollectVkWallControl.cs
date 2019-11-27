using System;
using System.Linq;
using System.Windows.Forms;
using PuppeteerService;
using ScenarioApp.Controls.Interfaces;
using ScenarioApp.Data;
using ScenarioApp.Ninject;
using ScenarioService;

namespace ScenarioApp.Controls
{
    public partial class CollectVkWallControl : UserControl, ICollectVkWallControl
    {
        private readonly IAccountDataLoader _accountDataLoader;

        public CollectVkWallControl(IAccountDataLoader accountDataLoader)
        {
            _accountDataLoader = accountDataLoader;
            InitializeComponent();
            button1.Click += button1_Click;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
                textBox2.Clear();
                var progressLog = new Progress<string>(update => textBox2.AppendText(update + Environment.NewLine));
                var vkWall = new CollectVkWall(progressLog: progressLog, chromiumSettings: CompositionRoot.Resolve<IChromiumSettings>());
                await vkWall.RunScenario(accountData: _accountDataLoader.GetAccountData().FirstOrDefault(), vkAccountNames: textBox1.Lines, pageCount: (int)numericUpDown1.Value);
        }
    }
}
