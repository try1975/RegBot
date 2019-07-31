using System;
using System.Net;
using System.Windows.Forms;
using AccountData.Service;
using AutoMapper;
using Common.Service.Enums;
using Common.Service.Interfaces;
using Gmail.Bot;
using log4net;
using MailRu.Bot;
using Newtonsoft.Json;
using PuppeteerSharp;
using RegBot.Db.Entities;
using RegBot.Db.Entities.QueryProcessors;
using RegBot.Demo.Ninject;
using Yandex.Bot;

namespace RegBot.Demo
{
    public partial class Form1 : Form
    {
        private CountryCode _countryCode = CountryCode.RU;
        private static readonly ILog Log = LogManager.GetLogger(typeof(Form1));
        private IAccountDataQuery accountDataQuery;
        private long BytesReceived { get; set; }

        public Form1()
        {
            InitializeComponent();

            accountDataQuery = CompositionRoot.Resolve<IAccountDataQuery>();

            GetRandomAccountData();
            cmbSmsService.DataSource = SmsServiceItem.GetSmsServiceItems();
            cmbSmsService.DisplayMember = "Text";
            cmbSmsService.SelectedIndex = 0;
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
            };
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

        private void BtnMailRu_Click(object sender, EventArgs e)
        {
            Demo(MailServiceCode.MailRu);
        }

        private IAccountData StoreData(IAccountData accountData)
        {
            var accountDataEntity = Mapper.Map<AccountDataEntity>(accountData);
            accountDataEntity = accountDataQuery.InsertEntity(accountDataEntity);
            return Mapper.Map<IAccountData>(accountDataEntity);
        }

        private async void Demo(MailServiceCode mailServiceCode)
        {
            try
            {
                var smsService = ((SmsServiceItem)cmbSmsService.SelectedItem).SmsService;
                textBox1.AppendText($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                IBot iBot = null;
                var accountData = StoreData(CreateEmailAccountDataFromUi());
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

                //_countryCode = CountryCode.KZ;
                if (iBot != null) accountData = await iBot.Registration(_countryCode);
                accountDataQuery.UpdateEntity(Mapper.Map<AccountDataEntity>(accountData));
                textBox1.AppendText($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)}... {JsonConvert.SerializeObject(accountData)} {Environment.NewLine}");
                textBox1.AppendText($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
            }
            catch (Exception exception)
            {
                textBox1.AppendText($"{exception}");
            }
        }

        private void BtnYandex_Click(object sender, EventArgs e)
        {
            Demo(MailServiceCode.Yandex);
        }

        private void btnGmail_Click(object sender, EventArgs e)
        {
            Demo(MailServiceCode.Gmail);
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            GetRandomAccountData();
        }
    }
}
