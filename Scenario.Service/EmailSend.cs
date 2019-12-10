using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using PuppeteerService;
using Yandex.Bot;

namespace ScenarioService
{
    public class EmailSend : ScenarioBase
    {
        private static readonly List<string> MailRuDomains = new List<string>() { "list.ru", "mail.ru", "inbox.ru", "bk.ru" };
        private readonly IProgress<string> _progressResult;

        public EmailSend(IChromiumSettings chromiumSettings, IProgress<string> progressLog, IProgress<string> progressResult)
            : base(typeof(EmailSend), chromiumSettings, progressLog)
        {
            _progressResult = progressResult;
        }

        public async Task RunScenario(List<LoginPasswordInput> listEmailAndPassword, string to, string subject, string[] emailText)
        {
            using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless()))
                foreach (var emailAndPassword in listEmailAndPassword)
                {
                    MailAddress mailAddress;
                    try
                    {
                        mailAddress = new MailAddress(emailAndPassword.Login);
                    }
                    catch (FormatException)
                    {
                        mailAddress = null;
                    }

                    if (mailAddress != null)
                    {
                        if (MailRuDomains.Contains(mailAddress.Host))
                        {
                            Info($"mailru {mailAddress.Address} {emailAndPassword.Password}");
                        }
                        if (mailAddress.Host.Equals("yandex.ru"))
                        {
                            Info($"yandex {mailAddress.Address} {emailAndPassword.Password}");
                            using (var page = await browser.NewPageAsync())
                            {
                                await page.GoToAsync(YandexRegistration.GetLoginUrl());
                                var isLoginSuccess = await YandexRegistration.Login(accountName: emailAndPassword.Login, password: emailAndPassword.Password, page: page);
                                await YandexRegistration.SendEmail(to: to, subject: subject, text: emailText, page: page);
                            }
                        }
                        if (mailAddress.Host.Equals("gmail.com"))
                        {
                            Info($"gmail {mailAddress.Address} {emailAndPassword.Password}");
                        }
                    }
                }
        }
    }
}
