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
            }
        }

        public BrowserProfileControl()
        {
            InitializeComponent();
            btnOk.Click += BtnOk_Click;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            _browserProfile.Name = tbName.Text;
            _browserProfile.UserAgent = tbUserAgent.Text;
            _browserProfile.StartUrl = tbStartUrl.Text;
        }


        public void SetData(IBrowserProfile browserProfile)
        {
            BrowserProfile = browserProfile;
        }
    }
}
