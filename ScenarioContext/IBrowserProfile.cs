﻿using PuppeteerService;
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

        Task<Browser> ProfileStart(string chromiumPath, string profilesPaths);
    }
}
