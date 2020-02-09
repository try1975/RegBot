﻿using Common.Service;
using PuppeteerService;
using ScenarioApp.Controls.Interfaces;
using ScenarioApp.Ninject;
using ScenarioService;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ScenarioApp.Controls
{
    public partial class ForumSearchControl : UserControl, IForumSearchControl
    {
        public ForumSearchControl()
        {
            InitializeComponent();
            button3.Click += button3_Click;
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Utils.SaveLinesToFile(textBox5.Lines);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
            //var progress = new Progress<string>(update => textBox5.AppendText(update + Environment.NewLine));
            var progress = new Progress<string>(update => ProgressChangedEventHandler(update));
            var queries = textBox6.Lines.Select(z => $"{z} доска объявлений форум").ToArray();
            var chromiumSettings = CompositionRoot.Resolve<IChromiumSettings>();
            if (!string.IsNullOrEmpty(tbYandexProxy.Text))
            {
                chromiumSettings.Proxy = tbYandexProxy.Text;
            }
            var yandexSearch = new YandexSearch(chromiumSettings: chromiumSettings, progressLog: progress);
            await yandexSearch.RunScenario(queries: queries, pageCount: (int)udPageCount.Value);
            chromiumSettings = CompositionRoot.Resolve<IChromiumSettings>();
            if (!string.IsNullOrEmpty(tbGoogleProxy.Text))
            {
                chromiumSettings.Proxy = tbGoogleProxy.Text;
            }
            var googleSearch = new GoogleSearch(chromiumSettings: chromiumSettings, progressLog: progress);
            await googleSearch.RunScenario(queries: queries, pageCount: (int)udPageCount.Value);
        }

        private void ProgressChangedEventHandler(string update)
        {
            textBox5.AppendText(update + Environment.NewLine);
        }
    }
}
