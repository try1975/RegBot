using Common.Service.Interfaces;
using Newtonsoft.Json;
using PuppeteerService;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ScenarioService
{
    public class CheckVkAccount : ScenarioBase
    {
        private readonly IProgress<CheckVkAccountOutput> _progressResult;

        public CheckVkAccount(IChromiumSettings chromiumSettings, IProgress<string> progressLog =null, IProgress<CheckVkAccountOutput> progressResult = null)
            : base(typeof(CheckVkAccount), chromiumSettings, progressLog)
        {
            _progressResult = progressResult;
        }

        public async Task<List<string>> RunScenario(IAccountData accountData, string[] vkAccountNames)
        {
            var result = new List<string>();
            try
            {
                using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless()))
                using (var page = await browser.NewPageAsync())
                {
                    if (accountData != null)
                    {
                        var vkAuthorizationAuth = await VkAuthorization.Auth(page, accountData);
                        Info($"Авторизация ВКонтакте успешна - {vkAuthorizationAuth}");
                    }
                    else
                    {
                        Info("Нет данных для авторизация ВКонтакте");
                    }
                    foreach (var vkAccountName in vkAccountNames)
                    {
                        if (string.IsNullOrEmpty(vkAccountName)) continue;
                        var response = await page.GoToAsync($"https://vk.com/{vkAccountName}");
                        var checkVkAccountResult = new CheckVkAccountOutput
                        {
                            VkAccountName = vkAccountName,
                            Available = (response.Status == HttpStatusCode.OK)
                        };
                        _progressResult?.Report(checkVkAccountResult);
                        var text = JsonConvert.SerializeObject(checkVkAccountResult);
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
