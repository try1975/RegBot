using Common.Service.Interfaces;
using Newtonsoft.Json;
using PuppeteerService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScenarioService
{
    public class CheckFbAccount : ScenarioBase
    {
        private readonly IProgress<CheckFbAccountOutput> _progressResult;

        public CheckFbAccount(IChromiumSettings chromiumSettings, IProgress<string> progressLog, Progress<CheckFbAccountOutput> progressResult)
            : base(typeof(CheckFbAccount), chromiumSettings, progressLog)
        {
            _progressResult = progressResult;
        }

        public async Task<List<string>> RunScenario(IAccountData accountData, string[] fbAccountNames)
        {
            var result = new List<string>();
            try
            {
                using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless()))
                using (var page = await browser.NewPageAsync())
                {
                    if (accountData != null)
                    {
                        var fbAuthorizationAuth = await FbAuthorization.Auth(page, accountData);
                        Info($"Авторизация Фейсбук - {fbAuthorizationAuth}");
                    }
                    else
                    {
                        Info("Нет данных для авторизация Фейсбук");
                    }
                    foreach (var fbAccountName in fbAccountNames)
                    {
                        if (string.IsNullOrEmpty(fbAccountName)) continue;
                        var response = await page.GoToAsync($"https://www.facebook.com/{fbAccountName}");
                        var checkFbAccountResult = new CheckFbAccountOutput
                        {
                            AccountName = fbAccountName,
                            Available = response.Ok
                        };
                        _progressResult?.Report(checkFbAccountResult);
                        var text = JsonConvert.SerializeObject(checkFbAccountResult);
                        Info(text);
                        result.Add(text);
                    }
                }
            }
            catch (Exception exception)
            {
                Error(exception.Message);
            }
            return result;
        }
    }
}
