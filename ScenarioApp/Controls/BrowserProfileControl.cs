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
using ScenarioContext;
using TimeZoneNames;

namespace ScenarioApp.Controls
{
    public partial class BrowserProfileControl : UserControl, IBrowserProfileControl
    {
        private IBrowserProfile _browserProfile;

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
            }
        }

        public BrowserProfileControl()
        {
            InitializeComponent();

            //cbLanguage.Items.Clear();
            //cbLanguage.Items.AddRange(TZNames.GetLanguageCodes().ToArray());
            cbCountry.DataSource = new BindingSource(TZNames.GetCountryNames("ru"), null);
            cbCountry.DisplayMember = "Value";
            cbCountry.ValueMember = "Key";
            cbCountry.SelectedIndex = -1;

            btnOk.Click += BtnOk_Click;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            _browserProfile.Name = tbName.Text;
            _browserProfile.UserAgent = tbUserAgent.Text;            _browserProfile.StartUrl = tbStartUrl.Text;
            _browserProfile.Language = cbLanguage.Text;
        }


        public void SetData(IBrowserProfile browserProfile)
        {
            BrowserProfile = browserProfile;
        }
    }
}
