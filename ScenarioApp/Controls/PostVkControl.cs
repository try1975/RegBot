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
    public partial class PostVkControl : UserControl, IPostVkControl
    {

        private readonly IAccountDataLoader _accountDataLoader;

        public PostVkControl(IAccountDataLoader accountDataLoader)
        {
            _accountDataLoader = accountDataLoader;
            InitializeComponent();
            button5.Click += button5_Click;
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            textBox9.Clear();
            var progress = new Progress<string>(update => textBox9.AppendText(update + Environment.NewLine));
            var postVk = new PostVk(chromiumSettings: CompositionRoot.Resolve<IChromiumSettings>(), progressLog: progress);
            await postVk.RunScenario(accountData:_accountDataLoader.VkAccount, vkGroups: textBox1.Lines, message: textBox10.Text);
        }
    }
}
