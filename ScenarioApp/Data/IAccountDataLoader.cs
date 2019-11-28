using Common.Service.Interfaces;
using System.Collections.Generic;

namespace ScenarioApp.Data
{
    public interface IAccountDataLoader
    {
        IList<IAccountData> GetVkAccountData();
        IList<IAccountData> GetFbAccountData();
    }
}
