using Common.Classes;
using Common.Service.Interfaces;
using Ninject.Modules;
using PuppeteerService;
using ScenarioApp.Controls;
using ScenarioApp.Controls.Interfaces;
using ScenarioApp.Data;
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
            Bind<ScenarioMain>().To<ScenarioMain>().InSingletonScope();
            Bind<IScenarioControl>().To<ScenarioControl>().InSingletonScope();
            Bind<IAccountDataLoader>().To<AccountDataLoader>().InSingletonScope();
            Bind<IDataSettings>().To<DataSettings>().InSingletonScope();

            var chromiumPath = Environment.CurrentDirectory;
            var userAgentGenerator = new UserAgentProvider();
            var proxyStore = new ProxyStore.Service.ProxyStore(Path.Combine(Application.StartupPath, ConfigurationManager.AppSettings["DbPath"]), Application.StartupPath);
            Bind<IChromiumSettings>().To<ChromiumSettings>().InTransientScope()/*.InSingletonScope()*/
                .WithConstructorArgument(nameof(chromiumPath), chromiumPath)
                .WithConstructorArgument(nameof(userAgentGenerator), userAgentGenerator)
                .WithConstructorArgument(nameof(proxyStore), proxyStore);
            
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
            Bind<IProxyControl>().To<ProxyControl>().InTransientScope()
                .WithConstructorArgument(nameof(proxyStore), proxyStore);
        }
    }
}
