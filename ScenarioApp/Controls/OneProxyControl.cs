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
using ProxyStore.Service;

namespace ScenarioApp.Controls
{
    public partial class OneProxyControl : UserControl, IOneProxyControl
    {
        public OneProxyControl()
        {
            InitializeComponent();
            btnCheckProxy.Click += BtnCheckProxy_Click;
        }

        private void BtnCheckProxy_Click(object sender, EventArgs e)
        {
            lbCheckProxy.Text = string.Empty;
            if (string.IsNullOrEmpty(tbProxy.Text)) return;
            var checkProxy = ProxyChecker.CheckProxy(tbProxy.Text);
            lbCheckProxy.Text = "Результат проверки ";
            if (checkProxy) lbCheckProxy.Text += "положительный"; else lbCheckProxy.Text += "отрицательный";
        }
    }
}
