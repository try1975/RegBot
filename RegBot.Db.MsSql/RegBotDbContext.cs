using System.Data.Entity;
using RegBot.Db.Entities;
using RegBot.Db.MsSql.Mappings;
using RegBot.Db.MsSql.Migrations;
using Z.EntityFramework.Plus;

namespace RegBot.Db.MsSql
{
    [DbConfigurationType(typeof(ConfigContext))]
    public class RegBotDbContext : DbContext
    {
        static RegBotDbContext()
        {
            AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) =>
               // ADD "Where(x => x.AuditEntryID == 0)" to allow multiple SaveChanges with same Audit
               ((RegBotDbContext) context).AuditEntries.AddRange(audit.Entries);
        }

        public RegBotDbContext() : base("name=RegBot")
        {
        }

        public virtual DbSet<AccountDataEntity> AccountsData { get; set; }

        public DbSet<AuditEntry> AuditEntries { get; set; }
        public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            const string prefix = "";
            modelBuilder.Configurations.Add(new AccountDataMap($"{prefix}{nameof(AccountsData)}"));
        }
    }

    public class ConfigContext : DbConfiguration
    {
        public ConfigContext()
        {
            SetDatabaseInitializer(new CreateDatabaseIfNotExists<RegBotDbContext>());
            SetDatabaseInitializer(new MigrateDatabaseToLatestVersion<RegBotDbContext, Configuration>());
        }
    }
}