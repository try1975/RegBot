using Common.Service.Interfaces;
using System.Collections.Generic;

namespace ScenarioApp.Data
{
    public interface IAccountDataLoader
    {
        IAccountData VkAccount { get; set; }
        IAccountData FbAccount { get; set; }
        IList<IAccountData> GetVkAccountData();
        IList<IAccountData> GetFbAccountData();
    }
}
