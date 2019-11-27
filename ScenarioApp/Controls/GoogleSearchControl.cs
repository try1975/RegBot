using PuppeteerService;
using ScenarioApp.Controls.Interfaces;
using ScenarioApp.Ninject;
using ScenarioService;
using System;
using System.Windows.Forms;

namespace ScenarioApp.Controls
{
    public partial class GoogleSearchControl : UserControl, IGoogleSearchControl
    {
        public GoogleSearchControl()
        {
            InitializeComponent();
            button2.Click += button2_Click;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            var progressLog = new Progress<string>(update => textBox3.AppendText(update + Environment.NewLine));
            var googleSearch = new GoogleSearch(chromiumSettings: CompositionRoot.Resolve<IChromiumSettings>(), progressLog: progressLog);
            await googleSearch.RunScenario(queries: textBox4.Lines, pageCount: (int)numericUpDown2.Value);
        }
    }
}
