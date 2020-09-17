using CheckHost;
using Common.Classes;
using Common.Service.Interfaces;
using Ip2location;
using IpCommon;
using Ninject.Modules;
using PuppeteerService;
using ScenarioApp.Controls;
using ScenarioApp.Controls.Interfaces;
using ScenarioApp.Data;
using ScenarioContext;
using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace ScenarioApp.Ninject
{
    public class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            var startupPath = Application.StartupPath;
            var chromiumPath = startupPath;
            chromiumPath = Path.Combine(chromiumPath, ".local-chromium\\Win64-706915\\chrome-win\\chrome.exe");

            Bind<ScenarioMain>().To<ScenarioMain>().InSingletonScope();
            Bind<IScenarioControl>().To<ScenarioControl>().InSingletonScope();
            Bind<IAccountDataLoader>().To<AccountDataLoader>().InSingletonScope();
            Bind<IDataSettings>().To<DataSettings>().InSingletonScope();

            var userAgentGenerator = new UserAgentProvider();
            Bind<IUserAgentProvider>().ToConstant(userAgentGenerator);
            var proxyStore = new ProxyStore.Service.ProxyStore(Path.Combine(startupPath, ConfigurationManager.AppSettings["DbPath"]), startupPath);
            Bind<IProxyStore>().ToConstant(proxyStore);

            Bind<IChromiumSettings>().To<ChromiumSettings>().InTransientScope()/*.InSingletonScope()*/
                .WithConstructorArgument(nameof(chromiumPath), chromiumPath);
                //.WithConstructorArgument(nameof(userAgentGenerator), userAgentGenerator)
                //.WithConstructorArgument(nameof(proxyStore), proxyStore);
            
            Bind<ISelectPersonControl>().To<SelectPersonControl>().InSingletonScope();
            Bind<ISmsServices>().To<SmsServices>().InSingletonScope().WithConstructorArgument("path", startupPath);

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


            Bind<IBrowserProfileControl>().To<BrowserProfileControl>().InTransientScope();
            Bind<IBrowserProfileService>().To<BrowserProfileService>().InSingletonScope()
                .WithConstructorArgument("chromiumPath", chromiumPath)
                .WithConstructorArgument("profilesPath", Path.Combine(startupPath, "Profiles"));

            Bind<IBrowserProfilesControl>().To<BrowserProfilesControl>().InTransientScope();
            

            Bind<IProxyControl>().To<ProxyControl>().InTransientScope();
            //.WithConstructorArgument(nameof(proxyStore), proxyStore);
            Bind<IFingerprintControl>().To<FingerprintControl>().InTransientScope();
            //.WithConstructorArgument(nameof(proxyStore), proxyStore);

            Bind<IIpInfoService>().To<Ip2LocationApi>().InSingletonScope();
            //Bind<IIpInfoService>().To <CheckHostApi>().InSingletonScope();
            Bind<IIpInfoControl>().To<IpInfoControl>().InTransientScope();
            Bind<IOneProxyControl>().To<OneProxyControl>().InTransientScope();
        }
    }
}
