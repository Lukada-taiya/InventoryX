using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Persistence
{
    public interface IBaseRepository<TEntity>
    {
        Task<int> Add(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> Get(int id);
        Task<int> Update(int id, TEntity entity);
        Task<int> Delete(int id);
    }
}
