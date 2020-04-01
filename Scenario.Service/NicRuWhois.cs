﻿using PuppeteerService;
using PuppeteerSharp;
using PuppeteerSharp.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScenarioService
{
    public class NicRuWhois : ScenarioBase
    {
        public NicRuWhois(IChromiumSettings chromiumSettings, IProgress<string> progressLog = null)
            : base(typeof(GoogleSearch), chromiumSettings, progressLog)
        {
        }

        public async Task<List<string>> RunScenario(string[] domains)
        {
            var result = new List<string>();
            try
            {
                using (var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless()))
                using (var page = await browser.NewPageAsync())
                {
                    foreach (var domain in domains)
                    {
                        Report($"Проверка {domain}");
                        await page.GoToAsync($"https://www.nic.ru/whois");
                        await page.TypeAsync("input", domain, new TypeOptions { Delay = 50 });
                        await page.Keyboard.PressAsync($"{nameof(Key.Enter)}");

                        var contentSelector = "div[data-qa='Whois-card']";
                        var data = await page.WaitForSelectorAsync(contentSelector, new WaitForSelectorOptions { Timeout = 5000 });
                        var text = (await (await data.GetPropertyAsync("innerText")).JsonValueAsync()).ToString();
                        var indexOf = text.IndexOf("<<<");
                        if (indexOf >= 0) text = text.Remove(indexOf);
                        Report($"{text}");
                        result.Add(text);
                    }
                }
            }
            catch (Exception exception)
            {
                Error(exception.ToString());
            }
            return result;
        }
    }
}
