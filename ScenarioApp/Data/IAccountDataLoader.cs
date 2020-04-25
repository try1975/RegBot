using Common.Service.Interfaces;
using System.Collections.Generic;

namespace ScenarioApp.Data
{
    public interface IAccountDataLoader
    {
        IAccountData VkAccount { get; set; }
        IAccountData FbAccount { get; set; }
        IAccountData MailruAccount { get; set; }
        IAccountData YandexAccount { get; set; }
        IAccountData GmailAccount { get; set; }
        IAccountData OkAccount { get; set; }

        IList<IAccountData> GetVkAccountData();
        IList<IAccountData> GetFbAccountData();
        IList<IAccountData> GetMailruAccountData();
        IList<IAccountData> GetYandexAccountData();
        IList<IAccountData> GetGmailAccountData();
        IList<IAccountData> GetOkAccountData();
    }
}
