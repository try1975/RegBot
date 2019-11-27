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
        public override void Load()
        {
            Bind<IScenarioControl>().To<ScenarioControl>().InSingletonScope();
            Bind<IAccountDataLoader>().To<AccountDataLoader>().InSingletonScope();
            Bind<IDataSettings>().To<DataSettings>().InSingletonScope();
            Bind<IChromiumSettings>().To<ChromiumSettings>().InSingletonScope().WithConstructorArgument("chromiumPath", Environment.CurrentDirectory);

            Bind<IRegBotControl>().To<RegBotControl>();

            Bind<ICollectVkWallControl>().To<CollectVkWallControl>();
            Bind<IGoogleSearchControl>().To<GoogleSearchControl>();
            Bind<IYandexSearchControl>().To<YandexSearchControl>();
            Bind<IWhoisControl>().To<WhoisControl>();
            Bind<IPostVkControl>().To<PostVkControl>();
            Bind<ICheckVkAccountControl>().To<CheckVkAccountControl>();
            Bind<ICheckVkCredentialControl>().To<CheckVkCredentialControl>();
            Bind<IEmailCheckControl>().To<EmailCheckControl>();
        }
    }
}
