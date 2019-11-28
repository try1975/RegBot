using Gmail.Bot;
using MailRu.Bot;
using PuppeteerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Yandex.Bot;

namespace ScenarioService
{
    public class EmailCheck : ScenarioBase
    {
        private static readonly List<string> MailRuDomains = new List<string>() { "list.ru", "mail.ru", "inbox.ru", "bk.ru" };
        public EmailCheck(IChromiumSettings chromiumSettings, IProgress<string> progressLog) : base(typeof(EmailCheck), chromiumSettings, progressLog)
        {
        }

        public async Task<List<string>> RunScenario(string[] emails)
        {
            var result = new List<string>();
            var listMailAddress = new Dictionary<string, MailAddress>(emails.Length);
            try
            {

                foreach (var email in emails.Where(x => !string.IsNullOrEmpty(x)).Select(x => x.ToLower()).Distinct().ToArray())
                {
                    MailAddress mailAddress;
                    try
                    {
                        mailAddress = new MailAddress(email);
                    }
                    catch (FormatException)
                    {
                        mailAddress = null;
                    }

                    if (mailAddress != null)
                    {
                        listMailAddress[mailAddress.Address] = mailAddress;
                        result.Add(mailAddress.Address);
                        Info($"{email} - успешная проверка написания");
                    }
                    else
                    {
                        Info($"{email} - ошибка написания");
                    }
                }
                
                //проверка попыткой регистрации
                using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless()))
                    foreach (var item in listMailAddress)
                    {
                        if (MailRuDomains.Contains(item.Value.Host))
                        {
                            using (var page = await browser.NewPageAsync())
                            {
                                await page.GoToAsync(MailRuRegistration.GetRegistrationUrl());
                                var emailAlreadyRegistered = await MailRuRegistration.EmailAlreadyRegistered(item.Value.User, item.Value.Host, page);
                                if (emailAlreadyRegistered)
                                {
                                    Info($"{item.Value.Address} - успешная проверка регистрацией - адрес существует");
                                }
                                else
                                {
                                    Info($"{item.Value.Address} - ошибка - проверка регистрацией - адрес не существует");
                                    result.Remove(item.Value.Address);
                                }
                            }
                        }
                        if (item.Value.Host.Equals("yandex.ru"))
                        {
                            using (var page = await browser.NewPageAsync())
                            {
                                await page.GoToAsync(YandexRegistration.GetRegistrationUrl());
                                var emailAlreadyRegistered = await YandexRegistration.EmailAlreadyRegistered(item.Value.User, page);
                                if (emailAlreadyRegistered)
                                {
                                    Info($"{item.Value.Address} - успешная проверка регистрацией - адрес существует");
                                }
                                else
                                {
                                    Info($"{item.Value.Address} - ошибка - проверка регистрацией - адрес не существует");
                                    result.Remove(item.Value.Address);
                                }
                            }
                        }
                        if (item.Value.Host.Equals("gmail.com"))
                        {
                            using (var page = await browser.NewPageAsync())
                            {
                                await page.GoToAsync(GmailRegistration.GetRegistrationUrl());
                                var emailAlreadyRegistered = await GmailRegistration.EmailAlreadyRegistered(item.Value.User, page, _chromiumSettings.GetPath());
                                if (emailAlreadyRegistered)
                                {
                                    Info($"{item.Value.Address} - успешная проверка регистрацией - адрес существует");
                                }
                                else
                                {
                                    Info($"{item.Value.Address} - ошибка - проверка регистрацией - адрес не существует");
                                    result.Remove(item.Value.Address);
                                }
                            }
                        }
                    }
            }
            catch (Exception exception)
            {
                Error(exception.ToString());
            }
            return result;
        }
    }
}
