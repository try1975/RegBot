using Common.Classes;
using Common.Service.Interfaces;
using Ninject.Modules;
using PuppeteerService;
using ScenarioApp.Controls;
using ScenarioApp.Controls.Interfaces;
using ScenarioApp.Data;
using System;

namespace ScenarioApp.Ninject
{
    public class ApplicationModule : NinjectModule
    {
        private string userAgent = System.Configuration.ConfigurationManager.AppSettings[nameof(userAgent)];
        private readonly Random _random = new Random();
        private int webkitVersion = 10;
        private int chromeVersion = 1000;
        private readonly string[] so = new string[] {
            "Windows NT 6.1; WOW64",
            "Windows NT 6.2; Win64; x64",
            "Windows NT 5.1; Win64; x64",
            "Macintosh; Intel Mac OS X 10_12_6",
            "X11; Linux x86_64",
            "X11; Linux armv7l"
        };

        public override void Load()
        {
            Bind<ScenarioMain>().To<ScenarioMain>().InSingletonScope();
            Bind<IScenarioControl>().To<ScenarioControl>().InSingletonScope();
            Bind<IAccountDataLoader>().To<AccountDataLoader>().InSingletonScope();
            Bind<IDataSettings>().To<DataSettings>().InSingletonScope();
            Bind<IChromiumSettings>().To<ChromiumSettings>()/*.InSingletonScope()*/
                .WithConstructorArgument("chromiumPath", Environment.CurrentDirectory)
                .WithConstructorArgument("userAgent", GetUserAgent());
            Bind<ISelectPersonControl>().To<SelectPersonControl>().InSingletonScope();
            Bind<ISmsServices>().To<SmsServices>().InSingletonScope().WithConstructorArgument("path", Environment.CurrentDirectory);

            Bind<IRegBotControl>().To<RegBotControl>();

            Bind<ICollectVkWallControl>().To<CollectVkWallControl>();
            Bind<IGoogleSearchControl>().To<GoogleSearchControl>();
            Bind<IYandexSearchControl>().To<YandexSearchControl>();
            Bind<IForumSearchControl>().To<ForumSearchControl>();
            Bind<IWhoisControl>().To<WhoisControl>();
            Bind<IPostVkControl>().To<PostVkControl>();
            Bind<ICheckVkAccountControl>().To<CheckVkAccountControl>();
            Bind<ICheckVkCredentialControl>().To<CheckVkCredentialControl>();
            Bind<IEmailCheckControl>().To<EmailCheckControl>();
            Bind<ICheckFbAccountControl>().To<CheckFbAccountControl>();
            Bind<ICheckFbCredentialControl>().To<CheckFbCredentialControl>();
            Bind<ICreateVkGroupControl>().To<CreateVkGroupControl>();
            Bind<ISendMailControl>().To<SendMailControl>();
            Bind<IGenerateAccountDataControl>().To<GenerateAccountDataControl>();
            Bind<ICaptchaControl>().To<CaptchaControl>();
            Bind<ISmsServiceControl>().To<SmsServiceControl>();
        }
        private string GetUserAgent()
        {
            webkitVersion++;
            chromeVersion++;
            return $"Mozilla / 5.0({so[_random.Next(0, so.Length)]}) AppleWebKit / 537.{webkitVersion} (KHTML, like Gecko) Chrome / 56.0.{chromeVersion}.87 Safari / 537.{webkitVersion} OPR / 43.0.2442.991";
        }

    }
}
