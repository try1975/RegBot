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

        public FingerprintControl(IChromiumSettings chromiumSettings, IProxyStore proxyStore)
        {
            InitializeComponent();
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
            try
            {
                var browser = await PuppeteerBrowser.GetBrowser(_chromiumSettings.GetPath(), _chromiumSettings.GetHeadless(), _chromiumSettings.GetArgs());
                var page = (await browser.PagesAsync())[0];
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
