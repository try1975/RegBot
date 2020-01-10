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
            Bind<ScenarioMain>().To<ScenarioMain>().InSingletonScope();
            Bind<IScenarioControl>().To<ScenarioControl>().InSingletonScope();
            Bind<IAccountDataLoader>().To<AccountDataLoader>().InSingletonScope();
            Bind<IDataSettings>().To<DataSettings>().InSingletonScope();
            Bind<IChromiumSettings>().To<ChromiumSettings>().InSingletonScope().WithConstructorArgument("chromiumPath", Environment.CurrentDirectory);
            Bind<ISelectPersonControl>().To<SelectPersonControl>().InSingletonScope();

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
        }
    }
}
