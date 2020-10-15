using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CallNodeJsFromCore
{
    public class CallNodeJsFromCoreApp
    {
        private readonly IJavaScriptService _javaScriptService;

        public CallNodeJsFromCoreApp(IJavaScriptService javaScriptService)
        {
            _javaScriptService = javaScriptService;
        }
        internal async Task<string> Run()
        {
            var result = await _javaScriptService.Hello("Michael");
            return result;
        }
    }
}
