using PuppeteerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ScenarioService
{
    public class EmailCheck : ScenarioBase
    {
        public EmailCheck(IChromiumSettings chromiumSettings, IProgress<string> progressLog) : base(typeof(EmailCheck), chromiumSettings, progressLog)
        {
        }

        public async Task<List<string>> RunScenario(string[] emails)
        {
            var result = new List<string>();
            var listMailAddress = new Dictionary<string, MailAddress>(emails.Length);
            try
            {

                foreach (var email in emails.Select(x=>x.ToLower()).Distinct().ToArray())
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
                        Info($"{email} - успешная проверка написания");
                    }
                    else
                    {
                        Info($"{email} - ошибка написания");
                    }
                }
                // проверка попыткой регистрации
                //using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless()))
                //using (var page = await browser.NewPageAsync())
                //{
                //    foreach (var item in listMailAddress)
                //    {
                //        if(item.Value.Host.Equals("mail.ru")|| item.Value.Host.Equals("list.ru"))
                //        {
                //            ;
                //        }
                //    }
                //}
            }
            catch (Exception exception)
            {
                Error(exception.ToString());
            }
            return result;
        }
    }
}
