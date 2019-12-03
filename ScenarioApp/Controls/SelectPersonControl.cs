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
            FillMailruAccount();
            FillYandexAccount();
            FillGmailAccount();

            cmbVkAccounts.DisplayMember = nameof(IAccountData.AccountName);
            cmbVkAccounts.SelectedIndex = 0;
            
            cmbFbAccounts.DisplayMember = nameof(IAccountData.AccountName);
            cmbFbAccounts.SelectedIndex = 0;

            cmbMailruAccounts.DisplayMember = nameof(IAccountData.AccountName);
            //if(cmbMailruAccounts!=null && (object[])cmbMailruAccounts.DataSource.l)
            //cmbMailruAccounts.SelectedIndex = 0;

            cmbYandexAccounts.DisplayMember = nameof(IAccountData.AccountName);
            //cmbYandexAccounts.SelectedIndex = 0;

            cmbGmailAccounts.DisplayMember = nameof(IAccountData.AccountName);
            //cmbGmailAccounts.SelectedIndex = 0;

            cmbVkAccounts.SelectionChangeCommitted += CmbVkAccounts_SelectionChangeCommitted;
            cmbFbAccounts.SelectionChangeCommitted += CmbFbAccounts_SelectionChangeCommitted;
            btnRefresh.Click += BtnRefresh_Click;
        }

       
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            var selectedAccount = _accountDataLoader.VkAccount;
            var comboBox = cmbVkAccounts;
            FillVkAccount();
            comboBox.SelectedIndex = comboBox.FindStringExact(selectedAccount.AccountName);

            selectedAccount = _accountDataLoader.FbAccount;
            comboBox = cmbFbAccounts;
            FillFbAccount();
            comboBox.SelectedIndex = comboBox.FindStringExact(selectedAccount.AccountName);

            selectedAccount = _accountDataLoader.MailruAccount;
            comboBox = cmbMailruAccounts;
            FillMailruAccount();
            comboBox.SelectedIndex = comboBox.FindStringExact(selectedAccount.AccountName);

            selectedAccount = _accountDataLoader.YandexAccount;
            comboBox = cmbMailruAccounts;
            FillYandexAccount();
            comboBox.SelectedIndex = comboBox.FindStringExact(selectedAccount.AccountName);

            selectedAccount = _accountDataLoader.GmailAccount;
            comboBox = cmbMailruAccounts;
            FillGmailAccount();
            comboBox.SelectedIndex = comboBox.FindStringExact(selectedAccount.AccountName);
        }

        private void CmbFbAccounts_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _accountDataLoader.FbAccount = (IAccountData)cmbFbAccounts.SelectedItem;
        }

        private void CmbMailrukAccounts_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _accountDataLoader.MailruAccount = (IAccountData)cmbMailruAccounts.SelectedItem;
        }

        private void CmbYandexAccounts_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _accountDataLoader.YandexAccount = (IAccountData)cmbYandexAccounts.SelectedItem;
        }

        private void CmbGmailAccounts_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _accountDataLoader.GmailAccount = (IAccountData)cmbGmailAccounts.SelectedItem;
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
        
        private void FillMailruAccount()
        {
            cmbMailruAccounts.DataSource = _accountDataLoader.GetMailruAccountData();
        }

        private void FillYandexAccount()
        {
            cmbYandexAccounts.DataSource = _accountDataLoader.GetYandexAccountData();
        }

        private void FillGmailAccount()
        {
            cmbGmailAccounts.DataSource = _accountDataLoader.GetGmailAccountData();
        }

    }
}
