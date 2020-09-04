using System;
using System.Windows.Forms;
using ScenarioApp.Controls.Interfaces;
using IpCommon;

namespace ScenarioApp.Controls
{
    public partial class IpInfoControl : UserControl, IIpInfoControl
    {
        private readonly IIpInfoService _ipInfoService;

        public string Ip { get => tbIp.Text; set => tbIp.Text = value; }
        public string TimezoneCountry { get => tbIpInfoTimezoneCountry.Text; set => tbIpInfoTimezoneCountry.Text=value; }
        public string Timezone { get => tbIpInfoTimezone.Text; set => tbIpInfoTimezone.Text = value; }
        public string Language { get => tbIpInfoLanguage.Text; set => tbIpInfoLanguage.Text=value; }

        public IpInfoControl(IIpInfoService ipInfoService)
        {
            _ipInfoService = ipInfoService;
            InitializeComponent();
            btnGetIpInfo.Click += BtnGetIpInfo_Click;
        }

        private async void BtnGetIpInfo_Click(object sender, EventArgs e)
        {
            TimezoneCountry = string.Empty;
            Timezone = string.Empty;
            Language = string.Empty;
            var ipInfo = await _ipInfoService.GetIpInfo(Ip);
            if (ipInfo == null) return;
            TimezoneCountry = ipInfo.CountryCode;
            Timezone = ipInfo.TimeZone;
            Language = ipInfo.LanguageCode;
        }
    }
}
