using System;
using System.Windows.Forms;
using AccountData.Service;
using Common.Service;
using Common.Service.Enums;
using ScenarioApp.Controls.Interfaces;

namespace ScenarioApp.Controls
{
    public partial class GenerateAccountDataControl : UserControl, IGenerateAccountDataControl
    {
        private readonly AccountDataGenerator _accountDataGenerator;

        public GenerateAccountDataControl()
        {
            InitializeComponent();
            _accountDataGenerator = new AccountDataGenerator(string.Empty);
            btnGenerateEn.Click += BtnGenerateEn_Click;
            btnGenerateRu.Click += BtnGenerateRu_Click;
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Utils.SaveLinesToFile(textBox3.Lines);
        }

        private void BtnGenerateEn_Click(object sender, EventArgs e)
        {
            Generate(CountryCode.EN);
        }
        private void BtnGenerateRu_Click(object sender, EventArgs e)
        {
            Generate(CountryCode.RU);
        }

        private void Generate(CountryCode countryCode)
        {
            //textBox3.Clear();
            for (int i = 0; i < udAccountCount.Value; i++)
            {
                var accountData = _accountDataGenerator.GetRandom(countryCode);
                textBox3.AppendText($"{accountData.Firstname} {accountData.Lastname} {accountData.BirthDate:d} {accountData.Sex} {accountData.Password} {Environment.NewLine}");
            }
        }
    }
}
