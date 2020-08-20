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
using PuppeteerSharp.Mobile;
using PuppeteerSharp;
using Common.Service.Enums;
using TimeZoneNames;

namespace ScenarioApp.Controls
{
    public partial class FingerprintControl : UserControl, IFingerprintControl
    {
        private readonly IChromiumSettings _chromiumSettings;
        private readonly IProxyStore _proxyStore;
        private DeviceItem _deviceItem;

        public FingerprintControl(IChromiumSettings chromiumSettings, IProxyStore proxyStore, IBrowserProfilesControl browserProfilesControl)
        {
            InitializeComponent();

            var control = (Control)browserProfilesControl;
            control.Dock = DockStyle.Fill;
            pnlProfiles.Controls.Clear();
            pnlProfiles.Controls.Add(control);

            _chromiumSettings = chromiumSettings;
            _proxyStore = proxyStore;

            btnWebShow.Click += BtnWebShow_Click;
            FillDeviceList(lbDevices);
            lbDevices.DoubleClick += LbDevices_DoubleClick;
            FillCountryList(cmbCountry);
            cmbCountry.SelectedIndexChanged += CmbCountry_SelectedIndexChanged;
            CmbCountry_SelectedIndexChanged(cmbCountry, null);
        }

        private void CmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbTimeZones.Items.Clear();
            var countryCode = Enum.GetName(typeof(CountryCode), ((CountryItem)cmbCountry.SelectedItem).CountryCode);
            var timeZonesForCountry = TZNames.GetTimeZonesForCountry(countryCode, "ru-ru");
            lbTimeZones.Items.AddRange(timeZonesForCountry.Keys.ToArray());
            if (lbTimeZones.Items.Count > 0) lbTimeZones.SelectedIndex = 0;
        }

        private void LbDevices_DoubleClick(object sender, EventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox == null) return;
            _deviceItem = listBox.SelectedItem as DeviceItem;
            if (_deviceItem == null) return;
            tbCurrentDevice.Text = _deviceItem.Text;
        }


        private async void BtnWebShow_Click(object sender, EventArgs e)
        {
            var url = cmbIpWeb.Text;
            var proxy = tbProxy.Text;
            try
            {
                _chromiumSettings.ClearArgs();
                if(lbLocale.SelectedIndex >= 0) _chromiumSettings.AddArg($"--lang={lbLocale.Items[lbLocale.SelectedIndex]}");
                _chromiumSettings.Proxy = proxy;
                var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless(), _chromiumSettings.GetArgs());
                var page = (await browser.PagesAsync())[0];
                await page.EvaluateFunctionOnNewDocumentAsync(@"() => {
        const originalFunction = HTMLCanvasElement.prototype.toDataURL;
        HTMLCanvasElement.prototype.toDataURL = function (type) {
            //if (type === 'image/png' && this.width === 220 && this.height === 30) {
                // this is likely a fingerprint attempt, return fake fingerprint
                
                return 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAANwAAAAeCAYAAABHenA+AAACiklEQVR4Xu3ZwW7DMAwD0PTP/ecburbAsMXr0yrDPbBnVrIoUlaSy9H/+5CQYwyBHYqjYDfQpYBtg44xlBc6n8bTApTnAo7qOI6DeOmuoxCP6tB+UDA93B1HBBYaV0z/FL6i5qdJtSFjDDqfxnt6sDugux9aRwynHZrjYrgTbtQgKlSNp+2M4c6Z6u4HTVNtWm64OVNqkO4Ga+9iuBjuiwEVggorz3BTYRGF2g8dHFkpifY/QVkps1JeB6VuT6QXlaUOhEI8qkM3GAqmh8tKmZXywUAMd66FGK44Tf4L1wmoQtV4el69GQo41VZuOG3SBEcEFhr34nF+/V2F0JpXDRLDvUa76kqzdPdjhfhiuDzD5Rlu4mg2nE5onRzdOJ1EhbdixI3ysmDyEoWFvFTvLv6o2I0g1Z+SfJ1YdHPtqlkL3iWYgvCJQo2nuMLnEtUBaevddUXNuH2+onoJdE367sRowTHcVEKqhRju/JGB+CNQDNfyul+HJeH05lJcbjiifQrSgR/DzXkmbvTmLwifOq/xFBfDEe0x3IMBnTBZKbNSvmat83+r/miKZ6XMSvmNgTzD5RnO3xLlhssNlxuugQG90mO4GK5Bbr9CqP6yUualyYMB1UJWyndaKdXp+nZPp5Hm1RtO3+4V8mopKmiKp3VQsAWg7vNpvF04nWr84VsFGMNN1RvDvWDsXUbSvDHcpLlKoA6YgoZiuAJZP6GFvlGW7ngxXAxHwlsFUkFrfo23CxfDxXCq5SU4Fb4m13i7cDFcDKdaXoJT4WtyjbcLF8PFcKrlJTgVvibXeLtwMVwMp1peglPha3KNtwsXw8VwquUlOBW+Jtd4u3AxXAynWl6CU+Frco23C/cJYv9dKep5EnQAAAAASUVORK5CYII=';
            //}
            // otherwise, just use the original function
            return originalFunction.apply(this, arguments);
        };
    }");
//                await page.EvaluateFunctionOnNewDocumentAsync(@"() => {
//        const getParameter = WebGLRenderingContext.getParameter;
//WebGLRenderingContext.prototype.getParameter = function(parameter) {
//  // UNMASKED_VENDOR_WEBGL
//  if (parameter === 37445) {
//    return 'Intel Open Source Technology Center';
//  }
//  // UNMASKED_RENDERER_WEBGL
//  if (parameter === 37446) {
//    return 'Mesa DRI Intel(R) Ivybridge Mobile ';
//  }

//  return getParameter(parameter);
//};
//    }");
                await PuppeteerBrowser.Authenticate(page, _chromiumSettings.Proxy);
                if (_deviceItem != null && !string.IsNullOrEmpty(tbCurrentDevice.Text)) await page.EmulateAsync(Puppeteer.Devices[_deviceItem.DeviceDescriptorName]);
                if(lbTimeZones.SelectedIndex>=0) await page.EmulateTimezoneAsync((string)lbTimeZones.Items[lbTimeZones.SelectedIndex]);
                await page.GoToAsync(url);
                browser = null;
                page = null;
            }
            catch (Exception exception)
            {
            }
        }

        private void FillDeviceList(ListBox listBox)
        {
            listBox.DataSource = DeviceItem.GetDeviceItems();
            listBox.DisplayMember = nameof(DeviceItem.Text);
            listBox.SelectedIndex = 0;
        }
        private void FillCountryList(ComboBox cmbCountry)
        {
            cmbCountry.DataSource = CountryItem.GetCountryItems();
            cmbCountry.DisplayMember = nameof(CountryItem.Text);
            cmbCountry.SelectedIndex = 0;
        }

    }
    public class DeviceItem
    {
        public string Text { get; set; }
        public DeviceDescriptorName DeviceDescriptorName { get; set; }

        public static List<DeviceItem> GetDeviceItems()
        {

            var list = new List<DeviceItem>(Enum.GetNames(typeof(DeviceDescriptorName)).Length);
            var values = Enum.GetValues(typeof(DeviceDescriptorName)).Cast<DeviceDescriptorName>();
            list.AddRange(values.Select(v => new DeviceItem { Text = Enum.GetName(typeof(DeviceDescriptorName), v), DeviceDescriptorName = v }));
            return list;
        }
    }
}
