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
using AccountData.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using Gmail.Bot;
using LiteDB;
using log4net;
using MailRu.Bot;
using Newtonsoft.Json;
using NickBuhro.Translit;
using PuppeteerSharp;
using Yandex.Bot;

namespace RegBot.Demo
{
    public partial class Form1 : Form
    {
        private CountryCode _countryCode = CountryCode.RU;
        private static readonly ILog Log = LogManager.GetLogger(typeof(Form1));
        private long BytesReceived { get; set; }
        private readonly string connectionString;

        public Form1()
        {
            InitializeComponent();

            GetRandomAccountData();
            cmbSmsService.DataSource = SmsServiceItem.GetSmsServiceItems();
            cmbSmsService.DisplayMember = "Text";
            cmbSmsService.SelectedIndex = 0;
            connectionString = Path.Combine(Application.StartupPath, ConfigurationManager.AppSettings["DbPath"]);
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

        private void Form1_Load(object sender, EventArgs e)
        {
            var browserFetcher = new BrowserFetcher();
            browserFetcher.DownloadProgressChanged += OnDownloadProgressChanged;
            GetBrowserLastVersion(browserFetcher);
        }

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
                Password = tbPassword.Text
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

        private async void Demo(MailServiceCode mailServiceCode)
        {
            try
            {
                var smsService = ((SmsServiceItem)cmbSmsService.SelectedItem).SmsService;
                textBox1.AppendText($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                IBot iBot = null;
                var accountData = CreateEmailAccountDataFromUi();
                //accountData.Firstname = "Иван";
                //accountData.Lastname = "Трефилов";
                //accountData.Domain = "list.ru";
                if (string.IsNullOrEmpty(accountData.AccountName))
                {
                    accountData.AccountName = Transliteration.CyrillicToLatin($"{accountData.Firstname.ToLower()}.{accountData.Lastname.ToLower()}", Language.Russian);
                }
                accountData = StoreAccountData(accountData);

                switch (mailServiceCode)
                {
                    case MailServiceCode.MailRu:
                        iBot = new MailRuRegistration(accountData, smsService, string.Empty);
                        break;
                    case MailServiceCode.Yandex:
                        iBot = new YandexRegistration(accountData, smsService, string.Empty);
                        break;
                    case MailServiceCode.Gmail:
                        iBot = new GmailRegistration(accountData, smsService, string.Empty);
                        break;
                }

                //_countryCode = CountryCode.UA;
                if (iBot != null) accountData = await iBot.Registration(_countryCode, headless: false);
                StoreAccountData(accountData);
                textBox1.AppendText($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)}... {JsonConvert.SerializeObject(accountData)} {Environment.NewLine}");
                textBox1.AppendText($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
            }
            catch (Exception exception)
            {
                textBox1.AppendText($"{exception}");
            }
        }

        private void BtnMailRu_Click(object sender, EventArgs e)
        {
            Demo(MailServiceCode.MailRu);
        }
        
        private void BtnYandex_Click(object sender, EventArgs e)
        {
            Demo(MailServiceCode.Yandex);
        }

        private void BtnGmail_Click(object sender, EventArgs e)
        {
            Demo(MailServiceCode.Gmail);
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            GetRandomAccountData();
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
    }
}
