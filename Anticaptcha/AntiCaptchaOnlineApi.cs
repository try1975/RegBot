using _AntiCaptcha;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnticaptchaOnline
{
    public class AntiCaptchaOnlineApi
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AntiCaptchaOnlineApi));
        private AntiCaptcha _apiKeyAnticaptcha = new AntiCaptcha(System.Configuration.ConfigurationManager.AppSettings[nameof(_apiKeyAnticaptcha)]);

        public AntiCaptchaResult SolveIm(string imageBase64)
        {
            var image = _apiKeyAnticaptcha.SolveImage(imageBase64).Result;
            return image;
        }
    }
}
