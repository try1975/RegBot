using _AntiCaptcha;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AnticaptchaOnline
{
    public class AntiCaptchaOnlineApi
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AntiCaptchaOnlineApi));
        private static readonly string _apiKeyAnticaptcha = ConfigurationManager.AppSettings[nameof(_apiKeyAnticaptcha)];
        private AntiCaptcha _anticaptcha;
        private IProgress<string> _progressLog;

        public AntiCaptchaOnlineApi()
        {
            _anticaptcha = new AntiCaptcha(GetApiKeyAnticaptcha());
        }

        public static string GetApiKeyAnticaptcha()
        {
            return _apiKeyAnticaptcha;
        }

        public AntiCaptchaOnlineApi(IProgress<string> progressLog)
        {
            _anticaptcha = new AntiCaptcha(GetApiKeyAnticaptcha());
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

        public async Task<string> SolveRecaptha(string googleSiteKey, string pageUrl)
        {
            var antiCaptchaResult = await _anticaptcha.SolveReCaptchaV2(googleSiteKey, pageUrl);
            if (antiCaptchaResult.Success) return antiCaptchaResult.Response;
            Log.Error(antiCaptchaResult.Response);
            return string.Empty;
        }


        public AntiCaptchaResult SolveIm(string imageBase64)
        {
            var image = _anticaptcha.SolveImage(imageBase64).Result;
            return image;
        }

        public async Task<string> SolveImPath(string path)
        {
            Info($"Решение капчи картинкой {path}");
            var antiCaptchaResult = await _anticaptcha.SolveImage(StringHelper.ImageFileToBase64String(path));
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
            var antiCaptchaResult = await _anticaptcha.GetBalance();
            if (!antiCaptchaResult.Success) return "0";
            return antiCaptchaResult.Response;
        }
    }
}
