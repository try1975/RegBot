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
using PuppeteerSharp;
using System.IO;
using System.Configuration;

namespace ScenarioApp.Controls
{
    public partial class CaptchaControl : UserControl, ICaptchaControl
    {
        private readonly AntiCaptchaOnlineApi _antiCaptchaOnlineApi;
        private readonly Progress<string> _progressLog;
        private readonly IChromiumSettings _chromiumSettings;
        private string _apiKeyAnticaptcha = ConfigurationManager.AppSettings[nameof(_apiKeyAnticaptcha)];

        public CaptchaControl()
        {
            InitializeComponent();
            
            _antiCaptchaOnlineApi = new AntiCaptchaOnlineApi(_progressLog);
            _progressLog = new Progress<string>(update => ProgressLogMethod(update));
            _chromiumSettings = CompositionRoot.Resolve<IChromiumSettings>();

            SetBalanceValue();

            btnAcRefreshBalance.Click += BtnAcRefreshBalance_Click;
            btnExecute.Click += BtnExecute_Click;
            btnImgLoad.Click += BtnImgLoad_Click;
            btnRecaptcha.Click += BtnRecaptcha_Click;
        }

        private async void BtnRecaptcha_Click(object sender, EventArgs e)
        {
            try
            {
                using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless(), _chromiumSettings.GetArgs()))
                using (var page = await PageInit(browser)) await Recaptcha(page);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        private async Task Recaptcha(Page page)
        {
            var eRecaptcha = await page.QuerySelectorAsync("#g-recaptcha-response");
            if (eRecaptcha != null)
            {
                var anticaptchaScriptText = File.ReadAllText(Path.GetFullPath(".\\Data\\init.js"));
                anticaptchaScriptText = anticaptchaScriptText.Replace("YOUR-ANTI-CAPTCHA-API-KEY", _apiKeyAnticaptcha);
                await page.EvaluateExpressionAsync(anticaptchaScriptText);

                //anticaptchaScriptText = File.ReadAllText(Path.GetFullPath(".\\Data\\1.js"));
                //await page.EvaluateExpressionAsync(anticaptchaScriptText);
                //await page.AddScriptTagAsync(new AddTagOptions { Content = anticaptchaScriptText });


                await page.AddScriptTagAsync("https://cdn.antcpt.com/imacros_inclusion/recaptcha.js");
                await page.WaitForSelectorAsync(".antigate_solver.solved", new WaitForSelectorOptions { Timeout = 120 * 1000 });
                await page.ClickAsync("input[type=submit]");
                //await page.WaitForNavigationAsync();
            }
            
        }

        private async Task<Page> PageInit(Browser browser, bool isIncognito = false)
        {
            Page page;
            if (isIncognito)
            {
                var context = await browser.CreateIncognitoBrowserContextAsync();
                page = await context.NewPageAsync();
            }
            else page = await browser.NewPageAsync();
            #region commented
            //await SetRequestHook(page);
            //await page.EmulateAsync(Puppeteer.Devices[DeviceDescriptorName.IPhone6]); 
            #endregion
            //await PuppeteerBrowser.Authenticate(page, _chromiumSettings.Proxy);
            await page.GoToAsync(GetInitUrl());
            return page;
        }

        public string GetInitUrl()
        {
            return cbUrl.Text;
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

                /*using (*/
                var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless(), _chromiumSettings.GetArgs()); /*)
                {*/
                /*using (*/                var page = await browser.NewPageAsync(); /*)
                    {*/

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
