using Common.Service;
using PuppeteerService;
using ScenarioApp.Controls.Interfaces;
using ScenarioApp.Ninject;
using ScenarioService;
using System;
using System.Windows.Forms;

namespace ScenarioApp.Controls
{
    public partial class YandexSearchControl : UserControl, IYandexSearchControl
    {
        public YandexSearchControl()
        {
            InitializeComponent();
            button3.Click += button3_Click;
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Utils.SaveLinesToFile(textBox5.Lines);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
            var progress = new Progress<string>(update => textBox5.AppendText(update + Environment.NewLine));
            var yandexSearch = new YandexSearch(chromiumSettings: CompositionRoot.Resolve<IChromiumSettings>(), progressLog: progress);
            await yandexSearch.RunScenario(queries: textBox6.Lines, pageCount: (int)numericUpDown3.Value);
        }
    }
}
