using EmailValidation;
using Gmail.Bot;
using MailRu.Bot;
using PuppeteerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Yandex.Bot;
using DnsLib;
using System.Collections;
using Common.Service;
using Common.Service.Enums;

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
                var dnsLite = new DnsLite();
                // https://developers.google.com/speed/public-dns/
                //http://www.opendns.com/opendns-ip-addresses/
                var dnslist = new List<string>
                {
                    "8.8.8.8",
                    "8.8.4.4",
                    "208.67.222.222",
                    "208.67.220.220"
                };

                var oldStyleList = new ArrayList();
                foreach (var s in dnslist)
                    oldStyleList.Add(s);
                dnsLite.setDnsServers(oldStyleList);

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
                        if (EmailValidator.Validate(mailAddress.Address))
                        {
                            var servers = dnsLite.getMXRecords(mailAddress.Host).OfType<MXRecord>().OrderBy(record => record.preference).Select(x => "smtp://" + x.exchange).Distinct().ToList();
                            //var servers = dnsLite.getMXRecords(mailAddress.Host).OfType<MXRecord>().Distinct().ToList();
                            if (servers.Count > 0)
                            {

                                listMailAddress[mailAddress.Address] = mailAddress;
                                result.Add(mailAddress.Address);
                                Info($"{email} - успешная проверка написания");
                            }
                            else {
                                Info($"{email} - ошибка Mx records");
                            }
                        }
                        else
                        {
                            Info($"{email} - ошибка rfc653x");
                        }
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
                                await page.GoToAsync(MailRuRegistration.RegistrationUrl);
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
                        if (item.Value.Host.Equals(ServiceDomains.GetDomain(ServiceCode.Yandex)))
                        {
                            using (var page = await browser.NewPageAsync())
                            {
                                await page.GoToAsync(YandexRegistration.RegistrationUrl);
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
                        if (item.Value.Host.Equals(ServiceDomains.GetDomain(ServiceCode.Gmail)))
                        {
                            using (var page = await browser.NewPageAsync())
                            {
                                await page.GoToAsync(GmailRegistration.RegistrationUrl);
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
