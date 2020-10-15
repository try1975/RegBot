using Microsoft.AspNetCore.NodeServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CallNodeJsFromCore
{
    public class JavaScriptService : IJavaScriptService
    {
        private readonly INodeServices _nodeServices;
        public readonly string _scriptFolder;
        public JavaScriptService(INodeServices nodeServices, string scriptFolder = "")
        {
            _nodeServices = nodeServices;
            _scriptFolder = scriptFolder;
        }

        public async Task<string> Hello(string name)
        {
            string path = Path.Combine(_scriptFolder, "./scripts/hello");
            var result = await _nodeServices.InvokeAsync<string>(path, name);
            return result;
        }
    }
}
