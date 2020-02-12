using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScenarioApp.Controls.Interfaces;
using PuppeteerService;
using ScenarioApp.Ninject;
using System.Diagnostics;

namespace ScenarioApp.Controls
{
    public partial class CaptchaControl : UserControl, ICaptchaControl
    {
        public CaptchaControl()
        {
            InitializeComponent();
            btnExecute.Click += BtnExecute_Click;
        }

        private async void BtnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                tbProgress.Clear();
                var progress = new Progress<string>(update => tbProgress.AppendText(update + Environment.NewLine));
                var _chromiumSettings = CompositionRoot.Resolve<IChromiumSettings>();
                //var domainCheck = new NicRuWhois(chromiumSettings: CompositionRoot.Resolve<IChromiumSettings>(), progressLog: progress);
                //await domainCheck.RunScenario(domains: textBox8.Lines);


                using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless()))
                {
                    using (var page = await browser.NewPageAsync())
                    {
                        await page.SetUserAgentAsync(_chromiumSettings.GetUserAgent());

                        await page.GoToAsync(cbUrl.Text);
                        await page.ExposeFunctionAsync("imgcl", async (string text) => 
                        {
                            Debug.WriteLine(text);
                            await page.EvaluateExpressionAsync($"console.log('{text}')");
                        //if (text.ToLower().Contains(args.TriggerWord) && !text.Contains(args.ResponseTemplate))
                        //{
                        //    await RespondAsync(args, text);
                        //}

                    });
                        //await Page.ExposeFunctionAsync("compute", (int a, int b) => a * b);
                        // var result = await Page.EvaluateExpressionAsync<int>("compute(9, 4)");
                        await page.EvaluateExpressionAsync("imgcl('фигня')");
                        await page.EvaluateExpressionAsync("imgcl('фигня')");

                        //await page.Mouse.DownAsync(new PuppeteerSharp.Input.ClickOptions { })
                    }
                }
            }
            catch (Exception exception)
            {

            }
        }


        private void pppFu(string text)
        {
            throw new NotImplementedException();
        }
    }
}
