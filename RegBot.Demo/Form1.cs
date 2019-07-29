using System;
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
        private readonly CountryCode _countryCode = CountryCode.RU;
        private static readonly ILog Log = LogManager.GetLogger(typeof(Form1));
        private IAccountDataQuery accountDataQuery;

        public Form1()
        {
            InitializeComponent();

            accountDataQuery = CompositionRoot.Resolve<IAccountDataQuery>();

            GetRandomAccountData();
            cmbSmsService.DataSource = SmsServiceItem.GetSmsServiceItems();
            cmbSmsService.DisplayMember = "Text";
            cmbSmsService.SelectedIndex = 0;
        }

        private static async void GetBrowserLastVersion()
        {
            try
            {
                await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = $@"GetBrowserLastVersion() - {DateTime.Now} {Environment.NewLine}";
            btnMailRu.Enabled = false;
            btnYandex.Enabled = false;
            GetBrowserLastVersion();
            var browserFetcher = new BrowserFetcher();
            textBox1.AppendText($@"GetExecutablePath - {browserFetcher.GetExecutablePath(BrowserFetcher.DefaultRevision)}{Environment.NewLine}");
            textBox1.AppendText($@"GetBrowserLastVersion() complete... - {DateTime.Now} {Environment.NewLine}");
            btnMailRu.Enabled = true;
            btnYandex.Enabled = true;
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

        private async void Demo(MailServiceCode mailServiceCode)
        {
            try
            {
                var smsService = ((SmsServiceItem)cmbSmsService.SelectedItem).SmsService;
                textBox1.AppendText($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)} start... - {DateTime.Now} {Environment.NewLine}");
                IBot iBot = null;
                var accountData = CreateEmailAccountDataFromUi();
                var accountDataEntity = Mapper.Map<AccountDataEntity>(accountData);
                accountDataEntity = accountDataQuery.InsertEntity(accountDataEntity);
                accountData = Mapper.Map<IAccountData>(accountDataEntity);
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

                if (iBot != null) accountData = await iBot.Registration(_countryCode);
                accountDataQuery.UpdateEntity(Mapper.Map<AccountDataEntity>(accountData));
                textBox1.AppendText($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)}... {JsonConvert.SerializeObject(accountData)} {Environment.NewLine}");
                textBox1.AppendText($@"{Enum.GetName(typeof(MailServiceCode), mailServiceCode)} finish... - {DateTime.Now} {Environment.NewLine}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
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
