using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace RegBot.Db.Entities
{
    public interface ITypedQuery<T, in TK> where T : class, IEntity<TK>
    {
        IQueryable<T> GetEntities();
        T GetEntity(TK id);
        Task<T> GetEntityAsync(TK id);
        T InsertEntity(T entity);
        T UpdateEntity(T entity);
        bool DeleteEntity(T entity);

        DbContext GetDbContext();
    }
}