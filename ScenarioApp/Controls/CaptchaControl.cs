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
using AnticaptchaOnline;

namespace ScenarioApp.Controls
{
    public partial class CaptchaControl : UserControl, ICaptchaControl
    {
        private readonly AntiCaptchaOnlineApi _antiCaptchaOnlineApi;
        private readonly Progress<string> _progressLog; 

        public CaptchaControl()
        {
            InitializeComponent();
            _progressLog = new Progress<string>(update => ProgressLogMethod(update));
            _antiCaptchaOnlineApi = new AntiCaptchaOnlineApi(_progressLog);

            SetBalanceValue();

            btnAcRefreshBalance.Click += BtnAcRefreshBalance_Click;
            btnExecute.Click += BtnExecute_Click;
            btnImgLoad.Click += BtnImgLoad_Click;
        }

        private void ProgressLogMethod(string update)
        {
            tbProgress.AppendText(update + Environment.NewLine);
        }

        private async void BtnImgLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            pictureBox1.ImageLocation = openFileDialog1.FileName;

            try
            {
                tbAcImgAnswer.Text = await _antiCaptchaOnlineApi.SolveImPath(openFileDialog1.FileName);
            }
            catch (Exception exception)
            {
                tbProgress.AppendText($"{exception}{Environment.NewLine}");
            }
        }

        private void BtnAcRefreshBalance_Click(object sender, EventArgs e)
        {
            SetBalanceValue();
        }

        private async void SetBalanceValue()
        {
            lblAcBalanceValue.Text = await _antiCaptchaOnlineApi.GetBalance();
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


                /*using (*/
                var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless()); /*)
                {*/
                /*using (*/                var page = await browser.NewPageAsync(); /*)
                    {*/
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


                        await page.EvaluateFunctionAsync(@"() => {
                            let imgs = Array.from(document.querySelectorAll('img'));
                            for (let i = 0; i < imgs.length; i++)
                            {
                                let a = imgs[i];
                                console.log(`Binding for ${ a.src}`);

                                // This event does not trigger!!!!!
                                a.addEventListener('click', e => {
                                    console.log(`some one clicked the img ${e}`);
                                    imgcl('aaaaa');
                                    e.preventDefault();
                                    e.stopPropagation();
                                    return false;
                                });
                             };
                           }");



                    //await page.Mouse.DownAsync(new PuppeteerSharp.Input.ClickOptions { })
                //}
                //}
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
