using AccountData.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using Facebook.Bot;
using Gmail.Bot;
using LiteDB;
using log4net;
using MailRu.Bot;
using Newtonsoft.Json;
using NickBuhro.Translit;
using PuppeteerSharp;
using ScenarioApp.Controls.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Vk.Bot;
using Yandex.Bot;

namespace ScenarioApp.Controls
{
    public partial class RegBotControl : UserControl, IRegBotControl
    {
        private CountryCode _countryCode = CountryCode.RU;
        private static readonly ILog Log = LogManager.GetLogger(typeof(RegBotControl));
        private long BytesReceived { get; set; }
        private readonly string connectionString;

        public RegBotControl()
        {
            InitializeComponent();

            GetRandomAccountData();

            cmbCountry.DataSource = CountryItem.GetCountryItems();
            cmbCountry.DisplayMember = "Text";
            cmbCountry.SelectedIndex = 0;

            connectionString = Path.Combine(Application.StartupPath, ConfigurationManager.AppSettings["DbPath"]);

            Load += Form1_Load;
            tabPage2.Enter += tabPage2_Enter;

            btnMailRu.Click += BtnMailRu_Click;
            btnYandex.Click += BtnYandex_Click;
            btnGmail.Click += BtnGmail_Click;
            btnFacebook.Click += btnFacebook_Click;
            btnVk.Click += BtnVk_Click;
            btnGenerate.Click += BtnGenerate_Click;
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

        private IAccountData CreateEmailAccountDataFromUi()
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

        private async void Demo(ServiceCode serviceCode)
        {
            try
            {
                var smsService = ((SmsServiceItem)cmbSmsService.SelectedItem).SmsService;
                textBox1.AppendText($@"{Enum.GetName(typeof(ServiceCode), serviceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                IBot iBot = null;
                var accountData = CreateEmailAccountDataFromUi();
                if (string.IsNullOrEmpty(accountData.AccountName))
                {
                    accountData.AccountName = Transliteration.CyrillicToLatin($"{accountData.Firstname.ToLower()}.{accountData.Lastname.ToLower()}", Language.Russian);
                }
                accountData = StoreAccountData(accountData);

                switch (serviceCode)
                {
                    case ServiceCode.MailRu:
                        iBot = new MailRuRegistration(accountData, smsService, string.Empty);
                        break;
                    case ServiceCode.Yandex:
                        iBot = new YandexRegistration(accountData, smsService, string.Empty);
                        break;
                    case ServiceCode.Gmail:
                        iBot = new GmailRegistration(accountData, smsService, string.Empty);
                        break;
                    case ServiceCode.Facebook:
                        iBot = new FacebookRegistration(accountData, smsService, string.Empty);
                        break;
                    case ServiceCode.Vk:
                        iBot = new VkRegistration(accountData, smsService, string.Empty);
                        break;
                }
                _countryCode = ((CountryItem)cmbCountry.SelectedItem).CountryCode;
                if (iBot != null) accountData = await iBot.Registration(_countryCode, headless: false);
                StoreAccountData(accountData);
                textBox1.AppendText($@"{Enum.GetName(typeof(ServiceCode), serviceCode)}... {JsonConvert.SerializeObject(accountData)} {Environment.NewLine}");
                textBox1.AppendText($@"{Enum.GetName(typeof(ServiceCode), serviceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
            }
            catch (Exception exception)
            {
                textBox1.AppendText($"{exception}");
            }
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

                btnMailRu.Enabled = false;
                btnYandex.Enabled = false;
                btnGmail.Enabled = false;
                textBox1.Text = $@"GetBrowserLastVersion() start... - {DateTime.Now} {Environment.NewLine}";
                await browserFetcher.DownloadAsync(BrowserFetcher.DefaultRevision);
                textBox1.AppendText($@"GetExecutablePath - {browserFetcher.GetExecutablePath(BrowserFetcher.DefaultRevision)}{Environment.NewLine}");
                textBox1.AppendText($@"GetBrowserLastVersion() complete... - {DateTime.Now} {Environment.NewLine}");
                btnMailRu.Enabled = true;
                btnYandex.Enabled = true;
                btnGmail.Enabled = true;
            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }
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

        private void BtnMailRu_Click(object sender, EventArgs e)
        {
            Demo(ServiceCode.MailRu);
        }

        private void BtnYandex_Click(object sender, EventArgs e)
        {
            Demo(ServiceCode.Yandex);
        }

        private void BtnGmail_Click(object sender, EventArgs e)
        {
            Demo(ServiceCode.Gmail);
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            GetRandomAccountData();
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var dataTable = ConvertToDataTable(db.GetCollection<IAccountData>("AccountsData").FindAll().OrderByDescending(z => z.Id));
                bindingSource1.DataSource = dataTable;
                advancedDataGridView1.DataSource = bindingSource1;
                //ContentGridColumnSettings(advancedDataGridView1);
            }
        }

        private void btnFacebook_Click(object sender, EventArgs e)
        {
            Demo(ServiceCode.Facebook);
        }

        private void BtnVk_Click(object sender, EventArgs e)
        {
            Demo(ServiceCode.Vk);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var smsServiceItems = SmsServiceItem.GetSmsServiceItems();
            cmbSmsService.DataSource = smsServiceItems;
            cmbSmsService.DisplayMember = "Text";
            cmbSmsService.SelectedIndex = 0;

            //var onlineSimRuApi = smsServiceItems.First(z => z.SmsServiceCode == SmsServiceCode.OnlineSimRu).SmsService;
            //var listSmsServiceInfo = new List<SmsServiceInfo>();
            //if (onlineSimRuApi != null)
            //{
            //  listSmsServiceInfo.AddRange(await onlineSimRuApi.GetInfo());  
            //}
            //File.AppendAllText(Path.Combine(Application.StartupPath, "Data", "SmsServiceInfo.json") ,JsonConvert.SerializeObject(listSmsServiceInfo)); 

            var browserFetcher = new BrowserFetcher();
            browserFetcher.DownloadProgressChanged += OnDownloadProgressChanged;
            GetBrowserLastVersion(browserFetcher);
        }
        #endregion Event handlers
    }
}
