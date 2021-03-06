﻿using AccountData.Service;
using Common.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using Facebook.Bot;
using Gmail.Bot;
using LiteDB;
using log4net;
using MailRu.Bot;
using Newtonsoft.Json;
using NickBuhro.Translit;
using Ok.Bot;
using Ig.Bot;
using PuppeteerService;
using PuppeteerSharp;
using ScenarioApp.Controls.Interfaces;
using ScenarioApp.Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vk.Bot;
using Yandex.Bot;
using Tw.Bot;
using ScenarioContext;

namespace ScenarioApp.Controls
{
    public partial class RegBotControl : UserControl, IRegBotControl
    {
        #region private fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(RegBotControl));
        private long BytesReceived { get; set; }
        private readonly string connectionString;
        private readonly ISmsServices _smsServices;
        private readonly IOneProxyControl _oneProxyControl;
        private readonly IBrowserProfileService _browserProfileService;
        #endregion

        public RegBotControl(ISmsServices smsServices, IOneProxyControl oneProxyControl, IBrowserProfileService browserProfileService)
        {
            InitializeComponent();

            _smsServices = smsServices;
            _oneProxyControl = oneProxyControl;
            _browserProfileService = browserProfileService;
            GetRandomAccountData(CountryCode.RU);

            cmbCountry.DataSource = CountryItem.GetCountryItems();
            cmbCountry.DisplayMember = "Text";
            cmbCountry.SelectedIndex = 0;

            connectionString = Path.Combine(Application.StartupPath, ConfigurationManager.AppSettings["DbPath"]);

            Load += Form1_Load;
            tabPage2.Enter += TabPage2_Enter;

            btnMailRuEmail.Click += BtnMailRuEmail_Click;
            btnMailRuPhone.Click += BtnMailRuPhone_Click;
            btnYandexEmail.Click += BtnYandexEmail_Click;
            btnYandexPhone.Click += BtnYandexPhone_Click;
            btnGmail.Click += BtnGmail_Click;
            btnFacebook.Click += BtnFacebook_Click;
            btnVk.Click += BtnVk_Click;
            btnOk.Click += BtnOk_Click;
            btnInstagram.Click += BtnInstagram_Click;
            btnTwitter.Click += BtnTwitter_Click;
            btnGenerateEn.Click += BtnGenerateEn_Click;
            btnGenerateRu.Click += BtnGenerateRu_Click;

            dgvItems.FilterStringChanged += DgvItems_FilterStringChanged;
            dgvItems.SortStringChanged += DgvItems_SortStringChanged;
            dgvItems.UserDeletingRow += DgvItems_UserDeletingRow;

            var control = (Control)_oneProxyControl;
            control.Dock = DockStyle.Fill;
            pnlOneProxy.Controls.Clear();
            pnlOneProxy.Controls.Add(control);
        }

        private async void FormLoad()
        {
            var smsServiceItems = SmsServiceItem.GetSmsServiceItems();
            cmbSmsService.DataSource = smsServiceItems;
            cmbSmsService.DisplayMember = "Text";
            cmbSmsService.SelectedIndex = 0;

            var browserProfileItems = BrowserProfileItem.GetItems(_browserProfileService);
            cmbBrowserProfile.DataSource = browserProfileItems;
            cmbBrowserProfile.DisplayMember = "Text";
            cmbBrowserProfile.SelectedIndex = 0;

            var browserFetcher = new BrowserFetcher();
            browserFetcher.DownloadProgressChanged += OnDownloadProgressChanged;
            GetBrowserLastVersion(browserFetcher);
            //init ServiceInfoList
            await _smsServices.GetServiceInfoList(ServiceCode.MailRu);

            foreach (SmsServiceCode smsServiceCode in Enum.GetValues(typeof(SmsServiceCode)))
            {
                var balance = await _smsServices.GetSmsService(smsServiceCode).GetBalance();
                if (balance < 5)
                {
                    textBox1.AppendText($@"Low balance {smsServiceCode} {balance} - {DateTime.Now} {Environment.NewLine}");
                    _smsServices.RemoveSmsServiceLowBalance(smsServiceCode);
                }
            }
        }

        private void DataTable_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            var accountdata = CreateItemFromRow<EmailAccountData>(e.Row);
            StoreAccountData(accountdata);
        }

        private void DgvItems_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row.Index == -1) return;
            var id = (int)e.Row.Cells["Id"].Value;
            using (var db = new LiteDatabase(connectionString))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<IAccountData>("AccountsData");
                col.Delete(id);
            }
        }

        private void DgvItems_SortStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Sort = dgvItems.SortString;
            bindingSource1.ResetBindings(false);
        }

        private void DgvItems_FilterStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Filter = dgvItems.FilterString;
            bindingSource1.ResetBindings(false);
        }

        private void GetRandomAccountData(CountryCode countryCode = CountryCode.EN)
        {
            var accountData = new AccountDataGenerator(string.Empty).GetRandom(countryCode);
            if (accountData == null) return;
            tbFirstName.Text = accountData.Firstname;
            tbLastname.Text = accountData.Lastname;
            tbPassword.Text = accountData.Password;
            dtpBirthDate.Value = accountData.BirthDate;
            rbMale.Checked = true;
            if (accountData.Sex == SexCode.Female) rbFemale.Checked = true;
        }

        private IAccountData CreateAccountDataFromUi()
        {
            return new EmailAccountData
            {
                Firstname = tbFirstName.Text,
                Lastname = tbLastname.Text,
                BirthDate = dtpBirthDate.Value,
                Sex = rbMale.Checked ? SexCode.Male : SexCode.Female,
                Password = tbPassword.Text,
                PhoneCountryCode = Enum.GetName(typeof(CountryCode), ((CountryItem)cmbCountry.SelectedItem).CountryCode)
            };
        }

        private IAccountData StoreAccountData(IAccountData accountData)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<IAccountData>("AccountsData");
                if (accountData.Id != 0)
                {
                    col.Update(accountData);
                }
                else
                {
                    var id = col.Insert(accountData).AsInt32;
                    accountData.Id = id;
                    accountData.CreatedAt = DateTime.Now;
                }
            }
            return accountData;
        }

        private static DataTable ConvertToDataTable<T>(IEnumerable<T> data)
        {

            var properties = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            try
            {
                foreach (PropertyDescriptor prop in properties)
                {
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
                if (data == null) return table;
                foreach (var item in data)
                {
                    var row = table.NewRow();

                    foreach (PropertyDescriptor prop in properties)
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }
                    table.Rows.Add(row);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                throw;
            }
            return table;
        }

        private async void GetBrowserLastVersion(BrowserFetcher browserFetcher)
        {
            try
            {
                BytesReceived = 0;

                btnMailRuEmail.Enabled = false;
                btnYandexEmail.Enabled = false;
                btnGmail.Enabled = false;
                textBox1.Text = $@"GetBrowserLastVersion() start... - {DateTime.Now} {Environment.NewLine}";
                await browserFetcher.DownloadAsync(BrowserFetcher.DefaultRevision);
                textBox1.AppendText($@"GetExecutablePath - {browserFetcher.GetExecutablePath(BrowserFetcher.DefaultRevision)}{Environment.NewLine}");
                textBox1.AppendText($@"GetBrowserLastVersion() complete... - {DateTime.Now} {Environment.NewLine}");
                btnMailRuEmail.Enabled = true;
                btnYandexEmail.Enabled = true;
                btnGmail.Enabled = true;
            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }
        }

        private async Task<IAccountData> Demo(ServiceCode serviceCode, SmsServiceCode? smsServiceCode = null, CountryCode? countryCode = null, bool byPhone = true)
        {
            try
            {
                var useChromiumSettings = cmbBrowserProfile.SelectedIndex == 0;
                var folder = ((BrowserProfileItem)cmbBrowserProfile.SelectedItem).Folder;

                ISmsService smsService = null;
                if (byPhone)
                {
                    if (smsServiceCode == null) smsService = _smsServices.GetSmsService(((SmsServiceItem)cmbSmsService.SelectedItem).SmsServiceCode);
                    else smsService = _smsServices.GetSmsService(smsServiceCode.Value);
                }
                textBox1.AppendText($@"{Enum.GetName(typeof(ServiceCode), serviceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                IBot iBot = null;
                var accountData = CreateAccountDataFromUi();
                if (string.IsNullOrEmpty(accountData.AccountName))
                {
                    accountData.AccountName = Transliteration.CyrillicToLatin($"{accountData.Firstname.ToLower()}.{accountData.Lastname.ToLower()}", Language.Russian);
                    accountData.AccountName = accountData.AccountName.Replace("`", "");
                }
                accountData = StoreAccountData(accountData);
                var chromiumSettings = CompositionRoot.Resolve<IChromiumSettings>();
                chromiumSettings.Proxy = $"{_oneProxyControl.ProxyRecord}";
                switch (serviceCode)
                {
                    case ServiceCode.MailRu:
                        iBot = new MailRuRegistration(accountData, smsService, chromiumSettings);
                        //iBot = new MailRuRegistration(accountData, smsService, _browserProfileService);
                        break;
                    case ServiceCode.Yandex:
                        iBot = new YandexRegistration(accountData, smsService, chromiumSettings);
                        break;
                    case ServiceCode.Gmail:
                        iBot = new GmailRegistration(accountData, smsService, chromiumSettings);
                        break;
                    case ServiceCode.Facebook:
                        accountData.Firstname = Transliteration.CyrillicToLatin(accountData.Firstname, Language.Russian);
                        accountData.Firstname = accountData.Firstname.Replace("`", "");
                        accountData.Lastname = Transliteration.CyrillicToLatin(accountData.Lastname, Language.Russian);
                        accountData.Lastname = accountData.Lastname.Replace("`", "");
                        iBot = new FacebookRegistration(accountData, smsService, chromiumSettings);
                        break;
                    case ServiceCode.Vk:
                        iBot = new VkRegistration(accountData, smsService, chromiumSettings);
                        break;
                    case ServiceCode.Ok:
                        iBot = new OkRegistration(accountData, smsService, chromiumSettings);
                        break;
                    case ServiceCode.Instagram:
                        iBot = useChromiumSettings ?
                            new InstagramRegistration(accountData, smsService, chromiumSettings) :
                            new InstagramRegistration(accountData, smsService, _browserProfileService, folder);
                        break;
                    case ServiceCode.Twitter:
                        iBot = useChromiumSettings ?
                            new TwitterRegistration(accountData, smsService, chromiumSettings) :
                            new TwitterRegistration(accountData, smsService, _browserProfileService, folder);
                        break;
                }
                if (countryCode == null) countryCode = ((CountryItem)cmbCountry.SelectedItem).CountryCode;
                if (iBot != null)
                {
                    textBox1.AppendText($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}... {JsonConvert.SerializeObject(accountData)} {Environment.NewLine}");
                    accountData = await iBot.Registration(countryCode.Value);
                }
                StoreAccountData(accountData);
                textBox1.AppendText($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}... {JsonConvert.SerializeObject(accountData)} {Environment.NewLine}");
                textBox1.AppendText($@"{Enum.GetName(typeof(ServiceCode), serviceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
                return accountData;
            }
            catch (Exception exception)
            {
                textBox1.AppendText($"{exception}");
            }
            return null;
        }

        private async void TryRegister(IEnumerable<SmsServiceInfo> infos)
        {
            if (infos == null || !infos.Any())
            {
                textBox1.AppendText($"Нет стоимостных данных смс сервисов - {DateTime.Now} {Environment.NewLine}");
                return;
            }
            foreach (var info in infos)
            {
                textBox1.AppendText($"Попытка {info.CountryCode} {info.ServiceCode} {info.SmsServiceCode} - {info.Price} руб - {DateTime.Now} {Environment.NewLine}");
                var accountData = await Demo(info.ServiceCode, info.SmsServiceCode, info.CountryCode);
                // if not no numbers then break
                if (accountData == null) break;
                if (accountData.Success) break;
                if (string.IsNullOrEmpty(accountData.ErrMsg)) break;
                if (accountData.ErrMsg.Equals(BotMessages.NoPhoneNumberMessage)) info.Skiped = true;
                if (!BotMessages.BadNumber.Contains(accountData.ErrMsg)) break;
                await _smsServices.AddFail(info);
            }
        }

        private SmsServiceInfoCondition GetSmsServiceInfoCondition(ServiceCode serviceCode)
        {
            if (cbSmsAuto.Checked && cbCountryAuto.Checked) return null;
            var smsServiceInfoCondition = new SmsServiceInfoCondition { ServiceCode = serviceCode };
            if (!cbCountryAuto.Checked)
            {
                smsServiceInfoCondition.CountryCodes = new List<CountryCode>
                {
                    ((CountryItem)cmbCountry.SelectedItem).CountryCode
                };
            }
            if (!cbSmsAuto.Checked)
            {
                smsServiceInfoCondition.SmsServiceCodes = new List<SmsServiceCode> 
                { 
                    ((SmsServiceItem)cmbSmsService.SelectedItem).SmsServiceCode
                };
            }
            return smsServiceInfoCondition;
        }

        #region Event handlers

        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (BytesReceived == 0)
            {
                textBox1.AppendText($@"Download size {e.TotalBytesToReceive} - {DateTime.Now} {Environment.NewLine}");
                BytesReceived = 1;
            }
            if (e.BytesReceived - BytesReceived <= 20000000) return;
            BytesReceived = e.BytesReceived;
            textBox1.AppendText($@"Download progress {e.BytesReceived} from {e.TotalBytesToReceive} - {DateTime.Now} {Environment.NewLine}");
        }

        private async void BtnMailRuEmail_Click(object sender, EventArgs e)
        {
            await Demo(ServiceCode.MailRu, byPhone: false);
        }

        private async void BtnMailRuPhone_Click(object sender, EventArgs e)
        {
            var smsServiceInfoCondition = GetSmsServiceInfoCondition(ServiceCode.MailRu);
            if (smsServiceInfoCondition != null) TryRegister(await _smsServices.GetServiceInfoList(smsServiceInfoCondition));
            else await Demo(ServiceCode.MailRu);
        }

        private async void BtnYandexEmail_Click(object sender, EventArgs e)
        {
            await Demo(ServiceCode.Yandex, byPhone: false);
        }

        private async void BtnYandexPhone_Click(object sender, EventArgs e)
        {
            var smsServiceInfoCondition = GetSmsServiceInfoCondition(ServiceCode.Yandex);
            if (smsServiceInfoCondition != null) TryRegister(await _smsServices.GetServiceInfoList(smsServiceInfoCondition));
            else await Demo(ServiceCode.Yandex);
        }

        private async void BtnGmail_Click(object sender, EventArgs e)
        {
            var smsServiceInfoCondition = GetSmsServiceInfoCondition(ServiceCode.Gmail);
            if (smsServiceInfoCondition != null) TryRegister(await _smsServices.GetServiceInfoList(smsServiceInfoCondition));
            else await Demo(ServiceCode.Gmail);
        }

        private async void BtnFacebook_Click(object sender, EventArgs e)
        {
            var smsServiceInfoCondition = GetSmsServiceInfoCondition(ServiceCode.Facebook);
            if (smsServiceInfoCondition != null) TryRegister(await _smsServices.GetServiceInfoList(smsServiceInfoCondition));
            else await Demo(ServiceCode.Facebook);
        }

        private async void BtnVk_Click(object sender, EventArgs e)
        {
            var smsServiceInfoCondition = GetSmsServiceInfoCondition(ServiceCode.Vk);
            if (smsServiceInfoCondition != null) TryRegister(await _smsServices.GetServiceInfoList(smsServiceInfoCondition));
            else await Demo(ServiceCode.Vk);
        }

        private async void BtnOk_Click(object sender, EventArgs e)
        {
            var smsServiceInfoCondition = GetSmsServiceInfoCondition(ServiceCode.Ok);
            if (smsServiceInfoCondition != null) TryRegister(await _smsServices.GetServiceInfoList(smsServiceInfoCondition));
            else await Demo(ServiceCode.Ok, byPhone: true);
        }

        private async void BtnInstagram_Click(object sender, EventArgs e)
        {
            var smsServiceInfoCondition = GetSmsServiceInfoCondition(ServiceCode.Instagram);
            if (smsServiceInfoCondition != null) TryRegister(await _smsServices.GetServiceInfoList(smsServiceInfoCondition));
            else await Demo(ServiceCode.Instagram, byPhone: true);
        }

        private async void BtnTwitter_Click(object sender, EventArgs e)
        {
            var smsServiceInfoCondition = GetSmsServiceInfoCondition(ServiceCode.Twitter);
            if (smsServiceInfoCondition != null) TryRegister(await _smsServices.GetServiceInfoList(smsServiceInfoCondition));
            else await Demo(ServiceCode.Twitter, byPhone: true);
        }

        private void BtnGenerateEn_Click(object sender, EventArgs e)
        {
            GetRandomAccountData();
        }
        private void BtnGenerateRu_Click(object sender, EventArgs e)
        {
            GetRandomAccountData(countryCode: CountryCode.RU);
        }

        private void TabPage2_Enter(object sender, EventArgs e)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var dataTable = ConvertToDataTable(db.GetCollection<IAccountData>("AccountsData").FindAll().OrderByDescending(z => z.Id));
                dataTable.ColumnChanged += DataTable_ColumnChanged;
                bindingSource1.DataSource = dataTable;
                dgvItems.DataSource = bindingSource1;
                //ContentGridColumnSettings(advancedDataGridView1);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormLoad();
        }
        #endregion Event handlers

        public static void SetItemFromRow<T>(T item, DataRow row) where T : new()
        {
            // go through each column
            foreach (DataColumn c in row.Table.Columns)
            {
                // find the property for the column
                PropertyInfo p = item.GetType().GetProperty(c.ColumnName);

                // if exists, set the value
                if (p != null && row[c] != DBNull.Value)
                {
                    p.SetValue(item, row[c], null);
                }
            }
        }

        // function that creates an object from the given data row
        public static T CreateItemFromRow<T>(DataRow row) where T : new()
        {
            // create a new object
            T item = new T();

            // set the item
            SetItemFromRow(item, row);

            // return 
            return item;
        }
    }
}
