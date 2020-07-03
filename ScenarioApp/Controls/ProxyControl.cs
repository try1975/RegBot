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
using Common.Service.Interfaces;
using PuppeteerService;
using System.IO;

namespace ScenarioApp.Controls
{
    public partial class ProxyControl : UserControl, IProxyControl
    {
        private readonly IChromiumSettings _chromiumSettings;
        private readonly IProxyStore _proxyStore;

        public ProxyControl(IChromiumSettings chromiumSettings, IProxyStore proxyStore)
        {
            InitializeComponent();
            _chromiumSettings = chromiumSettings;
            _proxyStore = proxyStore;
            
            if (_proxyStore != null)
            {
                lblProxyPath.Text = _proxyStore.GetPath();
                var proxyDataList = _proxyStore.GetProxies();
                tbProxies.Lines=proxyDataList.Select(x => x.ProxyString).ToArray();
            }
            tbProxies.DoubleClick += TbProxies_DoubleClick;
            btnIpWebShow.Click += BtnIpWebShow_Click;
            btnSaveProxy.Click += BtnSaveProxy_Click;
        }

        private void BtnSaveProxy_Click(object sender, EventArgs e)
        {
            try
            {
                var path = Path.GetFullPath(lblProxyPath.Text);
                File.WriteAllLines(path, tbProxies.Lines);
            }
            catch (Exception)
            {
            }
        }

        private void TbProxies_DoubleClick(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            var line = textBox.GetLineFromCharIndex(textBox.SelectionStart);
            var currentProxy = textBox.Lines[line];
            tbProxy.Text = currentProxy;
        }

        private async void BtnIpWebShow_Click(object sender, EventArgs e)
        {
            var url = cmbIpWeb.Text;
            var proxy = tbProxy.Text;
            try
            {
                _chromiumSettings.Proxy = proxy;
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

        private void LbProxies_DoubleClick(object sender, EventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox.SelectedItem == null) return;
            var currentProxy = (string)listBox.SelectedItem;
            tbProxy.Text = currentProxy;
        }
    }
}
