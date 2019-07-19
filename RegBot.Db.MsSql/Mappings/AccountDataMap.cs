using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RegBot.Db.Entities;

namespace RegBot.Db.MsSql.Mappings
{
    public class AccountDataMap : EntityTypeConfiguration<AccountDataEntity>
    {
        public AccountDataMap(string tableName)
        {
            HasKey(e => e.Id);
            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ToTable($"{tableName}");

            Property(e => e.ChangeBy)
                .HasMaxLength(50)
                ;
            Property(e => e.ChangeAt)
                .IsOptional()
                ;
        }
    }
}