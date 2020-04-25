using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Common.Service;
using Common.Service.Enums;
using MailRu.Bot;
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
                    try
                    {
                        var login = emailAndPassword.Login.Trim().ToLower();
                        var mailAddress = new MailAddress(login);
                        if (mailAddress != null)
                        {
                            if (MailRuDomains.Contains(mailAddress.Host))
                            {
                                using (var page = await browser.NewPageAsync())
                                {
                                    await page.GoToAsync(MailRuRegistration.GetLoginUrl());
                                    var isLoginSuccess = await MailRuRegistration.Login(accountName: login, password: emailAndPassword.Password, page: page);
                                    await MailRuRegistration.SendEmail(to: to, subject: subject, text: emailText, page: page);
                                    Info($"Почта отправлена {mailAddress.Address}");
                                }
                            }
                            if (mailAddress.Host.Equals(ServiceDomains.GetDomain(ServiceCode.Yandex)))
                            {
                                using (var page = await browser.NewPageAsync())
                                {
                                    await page.GoToAsync(YandexRegistration.GetLoginUrl());
                                    var isLoginSuccess = await YandexRegistration.Login(accountName: login, password: emailAndPassword.Password, page: page);
                                    await YandexRegistration.SendEmail(to: to, subject: subject, text: emailText, page: page);
                                    Info($"Почта отправлена {mailAddress.Address}");
                                }
                            }
                            if (mailAddress.Host.Equals(ServiceDomains.GetDomain(ServiceCode.Gmail)))
                            {
                                Info($"gmail {mailAddress.Address} {emailAndPassword.Password}");
                            }
                        }
                    }
                    catch (FormatException)
                    {
                        Error($"Некорректный адрес {emailAndPassword.Login}");
                    }
                }
        }
    }
}
