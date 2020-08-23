using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ScenarioApp.Controls.Interfaces;
using ScenarioContext;
using TimeZoneNames;

namespace ScenarioApp.Controls
{
    public partial class BrowserProfileControl : UserControl, IBrowserProfileControl
    {
        private IBrowserProfile _browserProfile;
        private readonly IDictionary<string, string> _timezoneCountries;
        private IDictionary<string, string> _timezones;

        public IBrowserProfile BrowserProfile
        {
            get => _browserProfile;
            set
            {
                _browserProfile = value;
                tbName.Text = _browserProfile.Name;
                tbFolder.Text = _browserProfile.Folder;
                tbUserAgent.Text = _browserProfile.UserAgent;
                tbStartUrl.Text = _browserProfile.StartUrl;
                cbLanguage.Text = _browserProfile.Language;
                cbTimezoneCountry.SelectedIndex = -1;
                cbTimezone.SelectedIndex = -1;
                if (!string.IsNullOrEmpty(_browserProfile.TimezoneCountry))
                {
                    //cbTimezoneCountry.SelectedItem = _timezoneCountries.Select(z => z.Key.Equals(_browserProfile.TimezoneCountry));
                    var idxTimezoneCountry = cbTimezoneCountry.FindStringExact(_timezoneCountries[_browserProfile.TimezoneCountry]);
                    cbTimezoneCountry.SelectedIndex = idxTimezoneCountry;
                    if (!string.IsNullOrEmpty(_browserProfile.Timezone))
                    {
                        var idxTimezone = cbTimezone.FindStringExact(_timezones[_browserProfile.Timezone]);
                        cbTimezone.SelectedIndex = idxTimezone;
                    }
                }
            }
        }

        public BrowserProfileControl()
        {
            InitializeComponent();

            _timezoneCountries = TZNames.GetCountryNames("ru");
            cbTimezoneCountry.DataSource = new BindingSource(_timezoneCountries, null);
            cbTimezoneCountry.DisplayMember = "Value";
            cbTimezoneCountry.ValueMember = "Key";

            btnOk.Click += BtnOk_Click;
            cbTimezoneCountry.SelectedIndexChanged += CbTimezoneCountry_SelectedIndexChanged;
        }



        private void BtnOk_Click(object sender, EventArgs e)
        {
            _browserProfile.Name = tbName.Text;
            _browserProfile.UserAgent = tbUserAgent.Text;
            _browserProfile.StartUrl = tbStartUrl.Text;
            _browserProfile.Language = cbLanguage.Text;
            _browserProfile.TimezoneCountry = null;
            _browserProfile.Timezone = null;
            if (cbTimezoneCountry.SelectedItem != null)
            {
                _browserProfile.TimezoneCountry = ((KeyValuePair<string, string>)cbTimezoneCountry.SelectedItem).Key;
                
                if (cbTimezone.SelectedItem != null)
                {
                    _browserProfile.Timezone = ((KeyValuePair<string, string>)cbTimezone.SelectedItem).Key;
                }
            }
        }
        private void CbTimezoneCountry_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void CbTimezoneCountry_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void CbTimezoneCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTimezoneCountry.SelectedItem != null)
            {
                var timezoneCountry = ((KeyValuePair<string, string>)cbTimezoneCountry.SelectedItem).Key;
                _timezones = TZNames.GetTimeZonesForCountry(timezoneCountry, "ru");
                cbTimezone.DataSource = new BindingSource(_timezones, null);
                cbTimezone.DisplayMember = "Value";
                cbTimezone.ValueMember = "Key";
            }
        }

        public void SetData(IBrowserProfile browserProfile) => BrowserProfile = browserProfile;
    }
}
