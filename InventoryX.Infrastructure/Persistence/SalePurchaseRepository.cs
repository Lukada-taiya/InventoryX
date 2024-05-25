using InventoryX.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Infrastructure.Persistence
{
    public class SalePurchaseRepository<T>(AppDbContext appDbContext) :  BaseRepository<T>(appDbContext), ISalePurchaseRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public override async Task<int> Add(T entity)
        { 
            await _appDbContext.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();

            var idProperty = typeof(T).GetProperty("Id") ?? throw new InvalidOperationException("Id property does not exist.");
            var idValue = (int)idProperty.GetValue(entity);

            return idValue;
        } 
    }
}
