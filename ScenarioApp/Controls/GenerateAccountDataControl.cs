using System;
using System.Windows.Forms;
using AccountData.Service;
using Common.Service;
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
            button2.Click += Button2_Click;
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Utils.SaveLinesToFile(textBox3.Lines);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            textBox3.Clear();

            for (int i = 0; i < udAccountCount.Value; i++)
            {
                var accountData = _accountDataGenerator.GetRandom();
                textBox3.AppendText($"{accountData.Firstname} {accountData.Lastname} {accountData.BirthDate:d} {accountData.Sex} {accountData.Password} {Environment.NewLine}");
            }
        }
    }
}
