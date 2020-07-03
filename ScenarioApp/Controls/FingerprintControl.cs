using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScenarioApp.Controls.Interfaces;
using PuppeteerService;
using Common.Service.Interfaces;

namespace ScenarioApp.Controls
{
    public partial class FingerprintControl : UserControl, IFingerprintControl
    {
        private readonly IChromiumSettings _chromiumSettings;
        private readonly IProxyStore _proxyStore;

        public FingerprintControl(IChromiumSettings chromiumSettings, IProxyStore proxyStore)
        {
            InitializeComponent();
            _chromiumSettings = chromiumSettings;
            _proxyStore = proxyStore;

            btnWebShow.Click += BtnWebShow_Click;
        }

        private async void BtnWebShow_Click(object sender, EventArgs e)
        {
            var url = cmbIpWeb.Text;
            try
            {
                var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless(), _chromiumSettings.GetArgs());
                var page = (await browser.PagesAsync())[0];
                await PuppeteerBrowser.Authenticate(page, _chromiumSettings.Proxy);
                await page.GoToAsync(url);
                browser = null;
                page = null;
            }
            catch (Exception exception)
            {
            }
        }
    }
}
