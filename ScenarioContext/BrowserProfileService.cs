using Common.Service;
using log4net;
using Newtonsoft.Json;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ScenarioContext
{
    public class BrowserProfileService : IBrowserProfileService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(BrowserProfileService));
        private readonly string _profilesJsonPath;
        private readonly List<BrowserProfile> _browserProfiles;
        private readonly string _chromiumPath;
        private readonly string _profilesPath;

        public BrowserProfileService(string chromiumPath, string profilesPath)
        {
            _chromiumPath = chromiumPath;
            _profilesPath = profilesPath;
            _profilesJsonPath = Path.Combine(_profilesPath, "profiles.json");
            try
            {
                _browserProfiles = JsonConvert.DeserializeObject<List<BrowserProfile>>(File.ReadAllText(_profilesJsonPath, System.Text.Encoding.UTF8));
            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }
            if (_browserProfiles == null) _browserProfiles = new List<BrowserProfile>();
            if (!_browserProfiles.Any())
            {
                _browserProfiles.Add(new BrowserProfile
                {
                    Name = Path.GetRandomFileName(),
                    Folder = Path.GetRandomFileName(),
                    ProxyRecord = new ProxyRecord()
                });
            }
        }

        public string Add(IBrowserProfile browserProfile)
        {
            _browserProfiles.Add((BrowserProfile)browserProfile);
            return browserProfile.Folder;
        }
        public IBrowserProfile GetNew()
        {
            var folder = Path.GetRandomFileName();
            var browserProfile = new BrowserProfile { Folder = folder };
            var path = Path.Combine(_profilesPath, folder);
            Directory.CreateDirectory(path);
            // copy fingerprint
            return browserProfile;
        }

        public IEnumerable<IBrowserProfile> GetBrowserProfiles() => _browserProfiles;

        public void SaveProfiles() => File.WriteAllText(_profilesJsonPath, JsonConvert.SerializeObject(_browserProfiles), System.Text.Encoding.UTF8);

        public void RemoveByFolder(string folder)
        {
            var _browserProfile = _browserProfiles.FirstOrDefault(z => z.Folder.Equals(folder));
            if (_browserProfile == null) return;
            _browserProfiles.Remove(_browserProfile);
            var directoryInfo = new DirectoryInfo(Path.Combine(_profilesPath, _browserProfile.Folder));
            try
            {
                directoryInfo.Delete(recursive: true);
            }
            catch (Exception exception)
            {
                Log.Error($"{exception}");
            }

        }

        public Task<Browser> StartProfile(string folder)
        {
            var browserProfile = _browserProfiles.FirstOrDefault(z => z.Folder.Equals(folder));
            if (browserProfile != null) return browserProfile.ProfileStart(_chromiumPath, _profilesPath);
            return null;
        }
    }
}
