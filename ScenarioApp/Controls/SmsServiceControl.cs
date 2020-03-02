using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ScenarioApp.Controls.Interfaces;
using Common.Service;
using Common.Service.Enums;
using Common.Service.Interfaces;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Diagnostics;

namespace ScenarioApp.Controls
{
    public partial class SmsServiceControl : UserControl, ISmsServiceControl
    {
        private readonly List<PhoneNumberRequestItem> _phoneNumberRequestItems = new List<PhoneNumberRequestItem>();
        private readonly BindingSource _requestsBindingSource = new BindingSource();
        private readonly BackgroundWorker _bw = new BackgroundWorker();
        private readonly Random _random = new Random();

        public SmsServiceControl()
        {
            InitializeComponent();

            cmbCountry.DataSource = CountryItem.GetCountryItems();
            cmbCountry.DisplayMember = "Text";
            cmbCountry.SelectedIndex = 0;

            var smsServiceItems = SmsServiceItem.GetSmsServiceItems();
            cmbSmsService.DataSource = smsServiceItems;
            cmbSmsService.DisplayMember = "Text";
            cmbSmsService.SelectedIndex = 0;

            _requestsBindingSource.DataSource = _phoneNumberRequestItems;
            dgvRequests.DataSource = _requestsBindingSource;

            btnExecute.Click += BtnExecute_Click;
            btnSave.Click += BtnSave_Click;

            _bw.WorkerSupportsCancellation = false;
            _bw.WorkerReportsProgress = true;
            _bw.DoWork += bw_DoWork;
            _bw.ProgressChanged += bw_ProgressChanged;
            _bw.RunWorkerAsync();
        }

        private async void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                try
                {
                    var id = (string)e.UserState;
                    var phoneNumberRequestItem = _phoneNumberRequestItems.FirstOrDefault(z => z.Id.Equals(id));
                    if (phoneNumberRequestItem == null) return;
                    var phoneNumberValidation = await phoneNumberRequestItem.SmsService.GetSmsOnes(id);
                    if (phoneNumberValidation == null) phoneNumberValidation = new PhoneNumberValidation();
                    if (!string.IsNullOrEmpty(phoneNumberValidation.Code))
                    {
                        phoneNumberRequestItem.Code = phoneNumberValidation.Code;
                        await phoneNumberRequestItem.SmsService.SetSmsValidationSuccess(phoneNumberRequestItem.Id);
                        tbLog.AppendText($"{DateTime.Now} {phoneNumberRequestItem.Id}. Получен код {phoneNumberRequestItem.Code} на номер {phoneNumberRequestItem.Phone}{Environment.NewLine}");
                    }
                    var remainSeconds = (int)(DateTime.UtcNow - phoneNumberRequestItem.Created).TotalSeconds;
                    if (remainSeconds > phoneNumberRequestItem.ActiveSeconds) remainSeconds = 0;
                    phoneNumberRequestItem.RemainSeconds = phoneNumberRequestItem.ActiveSeconds - remainSeconds;
                    _requestsBindingSource.ResetBindings(false);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine($"{exception}");
                }
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var cnt = 1;
            while (cnt > 0)
            {
                var tasks = _phoneNumberRequestItems.Where(z => z.RemainSeconds > 0).Select(z => z.Id).ToList();
                cnt = tasks.Count;
                foreach (var item in tasks)
                {
                    try
                    {
                        var bw = (BackgroundWorker)sender;
                        bw.ReportProgress(0, item);
                        Thread.Sleep(200);
                        _random.Next();
                    }
                    catch (Exception exception)
                    {
                        Debug.WriteLine($"{exception}");
                    }
                }
                Thread.Sleep(5000);
            }
        }

        private ServiceCode GetServiceCodeFromUi()
        {
            ServiceCode serviceCode = ServiceCode.Other;
            if (rbMailRu.Checked) serviceCode = ServiceCode.MailRu;
            if (rbYandex.Checked) serviceCode = ServiceCode.Yandex;
            if (rbGmail.Checked) serviceCode = ServiceCode.Gmail;
            if (rbFacebook.Checked) serviceCode = ServiceCode.Facebook;
            if (rbVk.Checked) serviceCode = ServiceCode.Vk;
            if (rbOk.Checked) serviceCode = ServiceCode.Ok;
            return serviceCode;
        }

        private async void BtnExecute_Click(object sender, EventArgs e)
        {
            var countryCode = ((CountryItem)cmbCountry.SelectedItem).CountryCode;
            var countryName = Enum.GetName(typeof(CountryCode), countryCode);

            ServiceCode serviceCode = GetServiceCodeFromUi();

            var smsServiceItem = (SmsServiceItem)cmbSmsService.SelectedItem;
            ISmsService smsService = smsServiceItem.SmsService;

            var smsServiceName = Enum.GetName(typeof(SmsServiceCode), smsServiceItem.SmsServiceCode);
            var serviceName = Enum.GetName(typeof(ServiceCode), serviceCode);
            tbLog.AppendText($"{DateTime.Now} Получение телефонного номера {smsServiceName} для сервиса {serviceName} и страны {countryName}{Environment.NewLine}");
            var phoneNumberRequest = await smsService.GetPhoneNumber(countryCode, serviceCode);
            //var phoneNumberRequest = new PhoneNumberRequest { Id = _random.Next().ToString(), Phone = "9046214577", Created = DateTime.UtcNow, ActiveSeconds = 900, RemainSeconds = 900 };
            if (phoneNumberRequest != null)
            {
                tbLog.AppendText($"{DateTime.Now} {phoneNumberRequest.Id}. Получен телефонного номер {phoneNumberRequest.Phone} {smsServiceName} для сервиса {serviceName} и страны {countryName}{Environment.NewLine}");
                _phoneNumberRequestItems.Add(new PhoneNumberRequestItem
                {
                    Id = phoneNumberRequest.Id,
                    Phone = phoneNumberRequest.Phone,
                    Created = phoneNumberRequest.Created,
                    ActiveSeconds = phoneNumberRequest.ActiveSeconds,
                    RemainSeconds = phoneNumberRequest.RemainSeconds,
                    SmsServiceCode = smsServiceItem.SmsServiceCode,
                    ServiceCode = serviceCode,
                    CountryCode = countryCode,
                    SmsService = smsService
                });
                _requestsBindingSource.ResetBindings(false);
                if (_bw.IsBusy != true) _bw.RunWorkerAsync();
                return;
            }
            tbLog.AppendText($"{DateTime.Now} Ошибка получения телефонного номера {smsServiceName} для сервиса {serviceName} и страны {countryName}{Environment.NewLine}");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Utils.SaveLinesToFile(tbLog.Lines);
        }
    }

    internal class PhoneNumberRequestItem
    {
        public string Id { get; set; }
        public string Phone { get; set; }
        public DateTime Created { get; set; }
        public int ActiveSeconds { get; set; }
        public int RemainSeconds { get; set; }
        public string Code { get; set; }
        public SmsServiceCode SmsServiceCode { get; set; }
        public ServiceCode ServiceCode { get; set; }
        public CountryCode CountryCode { get; set; }
        public ISmsService SmsService { get; set; }
    }
}
