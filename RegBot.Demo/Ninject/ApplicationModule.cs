using System.Data.Entity;
using Ninject.Modules;
using RegBot.Db.Entities.QueryProcessors;
using RegBot.Db.MsSql;
using RegBot.Db.MsSql.QueryProcessors;

namespace RegBot.Demo.Ninject
{
    public class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<RegBotDbContext>().InSingletonScope();
            Bind<IAccountDataQuery>().To<AccountDataQuery>().InSingletonScope();
        }
    }
}