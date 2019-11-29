using Common.Service.Interfaces;
using ScenarioApp.Controls.Interfaces;
using ScenarioApp.Data;
using System;
using System.Windows.Forms;

namespace ScenarioApp.Controls
{
    public partial class SelectPersonControl : UserControl, ISelectPersonControl
    {
        private readonly IAccountDataLoader _accountDataLoader;


        public SelectPersonControl(IAccountDataLoader accountDataLoader)
        {
            InitializeComponent();

            _accountDataLoader = accountDataLoader;
            FillVkAccount();
            FillFbAccount();

            cmbVkAccounts.DisplayMember = nameof(IAccountData.AccountName);
            cmbVkAccounts.SelectedIndex = 0;
            
            cmbFbAccounts.DisplayMember = nameof(IAccountData.AccountName);
            cmbFbAccounts.SelectedIndex = 0;

            cmbVkAccounts.SelectionChangeCommitted += CmbVkAccounts_SelectionChangeCommitted;
            cmbFbAccounts.SelectionChangeCommitted += CmbFbAccounts_SelectionChangeCommitted;
            btnRefresh.Click += BtnRefresh_Click;
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            var selectedVkAccount = _accountDataLoader.VkAccount;
            FillVkAccount();
            cmbVkAccounts.SelectedItem = selectedVkAccount;


            FillFbAccount();
        }

        private void CmbFbAccounts_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _accountDataLoader.FbAccount = (IAccountData)cmbFbAccounts.SelectedItem;
        }

        private void CmbVkAccounts_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _accountDataLoader.VkAccount = (IAccountData)cmbVkAccounts.SelectedItem;
        }

        private void FillVkAccount()
        {
            cmbVkAccounts.DataSource = _accountDataLoader.GetVkAccountData();
        }

        private void FillFbAccount()
        {
            cmbFbAccounts.DataSource = _accountDataLoader.GetFbAccountData();
            
        }
    }
}
