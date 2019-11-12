using Common.Service.Interfaces;
using LiteDB;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ScenarioApp.Scenario;

namespace ScenarioApp
{
    public partial class ScenarioMain : Form
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ScenarioMain));
        private readonly string chromiumPath = null;
        private readonly string connectionString;

        public ScenarioMain()
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
            var vkWall = new CollectVkWall(progress, accountsData.FirstOrDefault());
            await vkWall.RunScenario(chromiumPath: chromiumPath, headless: false, vkAccountName: textBox1.Text, vkPageCount: (int)numericUpDown1.Value);
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
            var domainCheck = new NicRuWhois(progress);
            await domainCheck.RunScenario(chromiumPath: chromiumPath, headless: false, domainName: textBox8.Text);
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

        private async void button5_Click(object sender, EventArgs e)
        {
            textBox9.Clear();
            var progress = new Progress<string>(update => textBox9.AppendText(update + Environment.NewLine));
            var accountsData = new List<IAccountData>();
            accountsData.AddRange(GetAccountData());
            var postVk = new PostVk(progress);
            await postVk.RunScenario(chromiumPath: chromiumPath, headless: false, accountsData.FirstOrDefault(), postText: textBox10.Text);
        }
    }
}
