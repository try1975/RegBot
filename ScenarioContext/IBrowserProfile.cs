using Common.Service;
using Common.Service.Interfaces;
using PuppeteerService;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScenarioContext
{
    public interface IBrowserProfile
    {
        string Name { get; set; }
        string Folder { get; set; }
        string UserAgent { get; set; }
        string StartUrl { get; set; }
        string Language { get; set; }
        string TimezoneCountry { get; set; }
        string Timezone { get; set; }
        ProxyRecord ProxyRecord { get; set; }

        Task<Browser> ProfileStart(string chromiumPath, string profilesPaths);
    }
}
