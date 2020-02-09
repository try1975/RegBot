using Common.Service;
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
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Utils.SaveLinesToFile(textBox3.Lines);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            var progress = new Progress<string>(update => textBox3.AppendText(update + Environment.NewLine));
            var chromiumSettings = CompositionRoot.Resolve<IChromiumSettings>();
            if (!string.IsNullOrEmpty(tbGoogleProxy.Text))
            {
                chromiumSettings.Proxy = tbGoogleProxy.Text;
            }
            var googleSearch = new GoogleSearch(chromiumSettings: chromiumSettings, progressLog: progress);
            await googleSearch.RunScenario(queries: textBox6.Lines, pageCount: (int)udPageCount.Value);
        }
    }
}
