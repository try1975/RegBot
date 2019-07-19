using System.Data.Entity;
using RegBot.Db.Entities;
using RegBot.Db.Entities.QueryProcessors;

namespace RegBot.Db.MsSql.QueryProcessors
{
    public class AccountDataQuery : TypedQuery<AccountDataEntity, int>, IAccountDataQuery
    {
        public AccountDataQuery(DbContext db) : base(db)
        {
        }
    }
}