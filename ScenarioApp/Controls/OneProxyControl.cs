using System;
using System.Windows.Forms;
using ScenarioApp.Controls.Interfaces;
using Common.Service;
using Common.Service.Enums;

namespace ScenarioApp.Controls
{
    public partial class OneProxyControl : UserControl, IOneProxyControl
    {
        public ProxyRecord ProxyRecord { get; private set; }
        public event EventHandler ProxyValueUpdated;

        public OneProxyControl()
        {
            InitializeComponent();
            ProxyRecord = new ProxyRecord();
            cbProxyProtocol.TextChanged += CbProxyProtocol_TextChanged;
            cbProxyProtocol.SelectedIndexChanged += CbProxyProtocol_SelectedIndexChanged;
            tbHost.TextChanged += TbHost_TextChanged;
            tbPort.TextChanged += TbPort_TextChanged;
            tbUsername.TextChanged += TbUsername_TextChanged;
            tbPassword.TextChanged += TbPassword_TextChanged;
            btnCheckProxy.Click += BtnCheckProxy_Click;
        }

        private void CbProxyProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Enum.TryParse<ProxyProtocol>(cbProxyProtocol.Text, out ProxyProtocol proxyProtocol))
            //{
            //    ProxyRecord.ProxyProtocol = proxyProtocol;
            //}
        }

        private void CbProxyProtocol_TextChanged(object sender, EventArgs e)
        {
            if (Enum.TryParse<ProxyProtocol>(cbProxyProtocol.Text.ToUpper(), out ProxyProtocol proxyProtocol))
            {
                if (ProxyRecord == null) return;
                if (ProxyRecord.ProxyProtocol != proxyProtocol) ProxyRecord.ProxyProtocol = proxyProtocol;
            }
        }

        private void TbHost_TextChanged(object sender, EventArgs e)
        {
            ProxyRecord.Host = tbHost.Text;
            ProxyValueUpdated?.Invoke(this, EventArgs.Empty);
        }
        private void TbPort_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(tbPort.Text, out int port);
            ProxyRecord.Port = port;
        }
        private void TbUsername_TextChanged(object sender, EventArgs e)
        {
            ProxyRecord.Username = tbUsername.Text;
        }
        private void TbPassword_TextChanged(object sender, EventArgs e)
        {
            ProxyRecord.Password = tbPassword.Text;
        }

        private void BtnCheckProxy_Click(object sender, EventArgs e)
        {
            //lbCheckProxy.Text = string.Empty;
            if (string.IsNullOrEmpty(tbHost.Text)) return;
            //var checkProxy = ProxyChecker.CheckProxy(tbHost.Text);
            //lbCheckProxy.Text = "Результат проверки ";
            //if (checkProxy) lbCheckProxy.Text += "положительный"; else lbCheckProxy.Text += "отрицательный";
        }

        public void SetProxyRecord(ProxyRecord proxyRecord)
        {
            ProxyRecord = proxyRecord.GetCopy();
            tbHost.Text = ProxyRecord.Host;
            tbPort.Text = $"{ProxyRecord.Port}";
            if (ProxyRecord.Port == 0) tbPort.Text = string.Empty;
            var proxyProtocol = Enum.GetName(typeof(ProxyProtocol), ProxyRecord.ProxyProtocol).ToLower();
            cbProxyProtocol.Text = proxyProtocol;
            tbUsername.Text = ProxyRecord.Username;
            tbPassword.Text = ProxyRecord.Password;
        }
    }
}
