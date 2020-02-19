using _AntiCaptcha;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnticaptchaOnline
{
    public class AntiCaptchaOnlineApi
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AntiCaptchaOnlineApi));
        private AntiCaptcha _apiKeyAnticaptcha = new AntiCaptcha(System.Configuration.ConfigurationManager.AppSettings[nameof(_apiKeyAnticaptcha)]);
        private IProgress<string> _progressLog;

        public AntiCaptchaOnlineApi()
        {

        }

        public AntiCaptchaOnlineApi(IProgress<string> progressLog)
        {
            _progressLog = progressLog;
        }

        protected void Info(string logMessage)
        {
            Log.Info(logMessage);
            Report(logMessage);
        }

        private protected void Report(string logMessage)
        {
            Debug.WriteLine(logMessage);
            _progressLog?.Report(logMessage);
        }

        public AntiCaptchaResult SolveIm(string imageBase64)
        {
            var image = _apiKeyAnticaptcha.SolveImage(imageBase64).Result;
            return image;
        }

        public async Task<string> SolveImPath(string path)
        {
            Info($"Решение капчи картинкой {path}");
            var antiCaptchaResult = await _apiKeyAnticaptcha.SolveImage(StringHelper.ImageFileToBase64String(path));
            if (antiCaptchaResult.Success)
            {
                Info($"Капча: {antiCaptchaResult.Response}");
                return antiCaptchaResult.Response;
            }
            Info("Ошибка решения капчи картинкой.");
            return string.Empty;
        }

        public async Task<string> GetBalance()
        {
            var antiCaptchaResult = await _apiKeyAnticaptcha.GetBalance();
            if (!antiCaptchaResult.Success) return "0";
            return antiCaptchaResult.Response;
        }
    }
}
