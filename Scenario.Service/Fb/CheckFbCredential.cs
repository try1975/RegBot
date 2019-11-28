using AccountData.Service;
using Newtonsoft.Json;
using PuppeteerService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScenarioService
{
    public class CheckFbCredential : ScenarioBase
    {
        private readonly IProgress<CheckFbCredentialOutput> _progressResult;

        public CheckFbCredential(IChromiumSettings chromiumSettings, IProgress<string> progressLog, Progress<CheckFbCredentialOutput> progressResult)
            : base(typeof(CheckFbCredential), chromiumSettings, progressLog)
        {
            _progressResult = progressResult;
        }

        public async Task<List<CheckFbCredentialOutput>> RunScenario(IList<CheckFbCredentialInput> listCheckFbCredentialInput)
        {
            var listCheckFbCredentialOutput = new List<CheckFbCredentialOutput>(listCheckFbCredentialInput.Count);
            foreach (var credential in listCheckFbCredentialInput)
            {
                try
                {
                    using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless()))
                    using (var page = await browser.NewPageAsync())
                    {
                        var pageUrl = await FbAuthorization.Auth(page, new EmailAccountData { Phone = credential.Login, Password = credential.Password });
                        var checkFbCredentialOutput = new CheckFbCredentialOutput
                        {
                            Login = credential.Login,
                            Password = credential.Password,
                            IsSuccess = !string.IsNullOrEmpty(pageUrl),
                            Url = pageUrl
                        };
                        listCheckFbCredentialOutput.Add(checkFbCredentialOutput);
                        _progressResult?.Report(checkFbCredentialOutput);
                        Info(JsonConvert.SerializeObject(checkFbCredentialOutput));
                    }
                }
                catch (Exception exception)
                {
                    Error(exception.Message);
                }
            }
            return listCheckFbCredentialOutput;
        }
    }
}
