using InventoryX.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Infrastructure.Persistence
{
    public class PurchaseRepository(AppDbContext appDbContext) :  BaseRepository<Purchase>(appDbContext), IPurchaseRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public override async Task<int> Add(Purchase purchase)
        {
             await _appDbContext.Purchases.AddAsync(purchase); 

            await _appDbContext.SaveChangesAsync();

            return _appDbContext.Purchases.OrderBy(p => p.Created_At).Last().Id;
        } 
    }
}
