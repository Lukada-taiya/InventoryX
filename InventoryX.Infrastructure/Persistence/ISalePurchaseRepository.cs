using InventoryX.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Infrastructure.Persistence
{
    public interface ISalePurchaseRepository<T> where T : class
    {
        Task<int> Add(T entity);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T> Get(int id, params Expression<Func<T, object>>[] includes);
        Task<int> Update(T entity);
        Task<int> Delete(int id);
    }
}
