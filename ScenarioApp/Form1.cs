using Common.Service.Interfaces;
using LiteDB;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ScenarioApp
{
    public partial class Form1 : Form
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Form1));
        private readonly string chromiumPath = null;
        private readonly string connectionString;

        public Form1()
        {
            InitializeComponent();

            connectionString = Path.Combine(Application.StartupPath, ConfigurationManager.AppSettings["DbPath"]);
            //var data = GetAccountData();
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            textBox2.Clear();
            var progress = new Progress<string>(update => textBox2.AppendText(update + Environment.NewLine));
            var accountsData = new List<IAccountData>();
            accountsData.AddRange(GetAccountData());
            var vkWall = new VkWall(chromiumPath: chromiumPath, progress: progress, accountData: accountsData.FirstOrDefault());
            await vkWall.Registration(vkAccountName: textBox1.Text, vkPageCount: (int)numericUpDown1.Value, headless: false);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            var progress = new Progress<string>(update => textBox3.AppendText(update + Environment.NewLine));
            var googleSearch = new GoogleSearch(chromiumPath: chromiumPath, progress: progress);
            await googleSearch.Registration(googleQuery: textBox4.Text, googlePageCount: (int)numericUpDown2.Value, headless: false);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
            var progress = new Progress<string>(update => textBox5.AppendText(update + Environment.NewLine));
            var yandexSearch = new YandexSearch(chromiumPath: chromiumPath, progress: progress);
            await yandexSearch.Registration(yandexQuery: textBox6.Text, yandexPageCount: (int)numericUpDown3.Value, headless: false);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            textBox7.Clear();
            var progress = new Progress<string>(update => textBox7.AppendText(update + Environment.NewLine));
            var domainCheck = new DomainCheck(chromiumPath: chromiumPath, progress: progress);
            await domainCheck.Registration(domainName: textBox8.Text, headless: false);
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                //var dataTable = ConvertToDataTable(db.GetCollection<IAccountData>("AccountsData").FindAll().OrderByDescending(z => z.Id));
                //bindingSource1.DataSource = dataTable;
                //advancedDataGridView1.DataSource = bindingSource1;
            }
        }

        private IEnumerable<IAccountData> GetAccountData()
        {
            using (var db = new LiteDatabase(connectionString))
            {
                return db.GetCollection<IAccountData>("AccountsData")
                    .Find(Query.And(Query.And(
                        Query.EQ(nameof(IAccountData.Domain), "vk.com"),
                        Query.EQ(nameof(IAccountData.Success), true)), Query.EQ(nameof(IAccountData.Sex), "Male")))
                ;
            }
        }
    }
}
