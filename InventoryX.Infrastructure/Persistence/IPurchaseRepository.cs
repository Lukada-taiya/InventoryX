using InventoryX.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Infrastructure.Persistence
{
    public interface IPurchaseRepository
    {
        Task<int> Add(Purchase purchase);
        Task<IEnumerable<Purchase>> GetAllAsync(params Expression<Func<Purchase, object>>[] includes);
        Task<Purchase> Get(int id, params Expression<Func<Purchase, object>>[] includes);
        Task<int> Update(Purchase entity);
        Task<int> Delete(int id);
    }
}
