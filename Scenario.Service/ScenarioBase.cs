using log4net;
using PuppeteerService;
using System;
using System.Diagnostics;

namespace ScenarioService
{
    public class ScenarioBase
    {

        private readonly ILog Log = LogManager.GetLogger(typeof(ScenarioBase));
        protected readonly IChromiumSettings _chromiumSettings;
        private readonly IProgress<string> _progressLog;

        public ScenarioBase(Type type, IChromiumSettings chromiumSettings, IProgress<string> progressLog)
        {
            if (type != null) Log = LogManager.GetLogger(type);
            _chromiumSettings = chromiumSettings;
            _progressLog = progressLog;
        }

        #region Report
        protected void Info(string logMessage)
        {
            Log.Info(logMessage);
            Report(logMessage);
        }

        protected  void Error(string logMessage)
        {
            Log.Error(logMessage);
            Report(logMessage);
        }

        private protected void Report(string logMessage)
        {
            Debug.WriteLine(logMessage);
            _progressLog?.Report(logMessage);
        }
        #endregion Report
    }
}
