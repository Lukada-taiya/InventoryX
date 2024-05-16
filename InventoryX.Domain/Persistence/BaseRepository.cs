using InventoryX.Application.Persistence;
using InventoryX.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Domain.Persistence
{
    public class BaseRepository<TEntity> (AppDbContext context): IBaseRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context = context;
        public async Task<int> Add(TEntity entity)
        { 
            var idProperty = typeof(TEntity).GetProperty("Id") ?? throw new InvalidOperationException("TEntity does not contain an Id property.");
            var idValue = idProperty.GetValue(entity); 
            var recordExists =  await _context.Set<TEntity>().FindAsync(idValue);
            if(recordExists is null)
            {
                return 0;
            }
            await _context.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        { 
            var entityToDelete = await _context.Set<TEntity>().FindAsync(id);

            if (entityToDelete is null)
            { 
                return 0;
            }
             
            _context.Set<TEntity>().Remove(entityToDelete);
             
            return await _context.SaveChangesAsync();
        }

        public async Task<TEntity> Get(int id) => await _context.FindAsync<TEntity>(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _context.Set<TEntity>().ToListAsync();
        public async Task<int> Update(int id, TEntity entity)
        { 
            var entityToUpdate = await _context.Set<TEntity>().FindAsync(id);

            if (entityToUpdate is null)
            { 
                return 0;
            }
             
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
             
            return await _context.SaveChangesAsync();
        }
    }
}
