using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Infrastructure.Persistence
{
    public interface IBaseRepository<TEntity>
    {
        Task<int> Add(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> Get(int id, params Expression<Func<TEntity, object>>[] includes);
        Task<int> Update(TEntity entity);
        Task<int> Delete(int id);
    }
}
