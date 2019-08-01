using Ninject.Modules;

namespace RegBot.Demo.Ninject
{
    public class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            //Bind<DbContext>().To<RegBotDbContext>().InSingletonScope();
            //Bind<IAccountDataQuery>().To<AccountDataQuery>().InSingletonScope();
        }
    }
}