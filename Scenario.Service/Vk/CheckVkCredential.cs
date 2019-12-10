using AccountData.Service;
using Newtonsoft.Json;
using PuppeteerService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScenarioService
{
    public class CheckVkCredential : ScenarioBase
    {
        private readonly IProgress<CheckVkCredentialOutput> _progressResult;

        public CheckVkCredential(IChromiumSettings chromiumSettings, Progress<string> progressLog = null, Progress<CheckVkCredentialOutput> progressResult = null)
            : base(typeof(CheckVkCredential), chromiumSettings, progressLog)
        {
            _progressResult = progressResult;
        }

        public async Task<List<CheckVkCredentialOutput>> RunScenario(IList<LoginPasswordInput> listLoginPasswordInput)
        {
            var listCheckVkCredentialOutput = new List<CheckVkCredentialOutput>(listLoginPasswordInput.Count);
            foreach (var credential in listLoginPasswordInput)
            {
                try
                {
                    using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless()))
                    using (var page = await browser.NewPageAsync())
                    {
                        var pageUrl = await VkAuthorization.Auth(page, new EmailAccountData { Phone = credential.Login, Password = credential.Password });
                        var checkVkCredentialOutput = new CheckVkCredentialOutput
                        {
                            Login = credential.Login,
                            Password = credential.Password,
                            IsSuccess = !string.IsNullOrEmpty(pageUrl),
                            Url = pageUrl
                        };
                        listCheckVkCredentialOutput.Add(checkVkCredentialOutput);
                        _progressResult?.Report(checkVkCredentialOutput);
                        Info(JsonConvert.SerializeObject(checkVkCredentialOutput));
                    }
                }
                catch (Exception exception)
                {
                    Error(exception.Message);
                }
            }
            return listCheckVkCredentialOutput;
        }
    }
}