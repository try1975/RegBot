using PuppeteerSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScenarioContext
{
    public interface IBrowserProfileService
    {
        IEnumerable<IBrowserProfile> GetBrowserProfiles();
        void SaveProfiles();
        string Add(IBrowserProfile browserProfile);
        IBrowserProfile GetNew();
        void RemoveByFolder(string folder);
        void AddToName(string folder, string text);
        Task<Browser> StartProfile(string folder);
    }
}
