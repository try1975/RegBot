using System;
using System.Linq;
using System.Windows.Forms;
using ScenarioApp.Controls.Interfaces;
using Common.Service;
using ScenarioApp.Ninject;
using PuppeteerService;
using ScenarioService;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace ScenarioApp.Controls
{
    public partial class CreateVkGroupControl : UserControl, ICreateVkGroupControl
    {
        private IProgress<string> _progressLog;
        private IProgress<string> _progressResult;

        public CreateVkGroupControl()
        {
            InitializeComponent();
            _progressLog = new Progress<string>(update => tbLog.AppendText(update + Environment.NewLine));
            _progressResult = new Progress<string>(update => tbResult.AppendText(update + Environment.NewLine));

            button1.Click += Button1_Click;
            btnSave.Click += BtnSave_Click;
            btnDownload.Click += BtnDownload_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Utils.SaveLinesToFile(tbResult.Lines.Distinct().ToArray());
        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                //textBox1.Text = folderBrowserDialog1.SelectedPath;
                var dowloadLinks = tbResult.Lines.Where(x => !string.IsNullOrEmpty(x)).Distinct();
                foreach (var url in dowloadLinks)
                {
                    DownloadFile(folderBrowserDialog1.SelectedPath, url);
                }
                Process.Start(folderBrowserDialog1.SelectedPath);
            }
        }

        private void DownloadFile(string path, string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.109 Safari/537.36";

            Directory.CreateDirectory(path ?? throw new InvalidOperationException());
            using (var response = (HttpWebResponse)request.GetResponseAsync().Result)
            {
                var filename = Path.GetRandomFileName();
                var contentDisposition = response.Headers["Content-Disposition"];
                if (!string.IsNullOrEmpty(contentDisposition))
                {
                    filename = contentDisposition.Substring(21).Trim();
                }
                filename = Path.Combine(path, filename);
                if (File.Exists(filename)) return;
                _progressLog?.Report($"Download: {filename}");
                Application.DoEvents();
                var responseStream = response.GetResponseStream();

                using (var fileStream = File.Create(filename))
                {
                    responseStream?.CopyTo(fileStream);
                }
            }
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            tbLog.Clear();
            var chromiumSettings = CompositionRoot.Resolve<IChromiumSettings>();
            var hunterSearch = new HunterSearch(chromiumSettings: chromiumSettings, progressLog: _progressLog, progressResult: _progressResult);
            await hunterSearch.RunScenario(queries: textBox1.Lines, pageCount: 0);
        }
    }
}
