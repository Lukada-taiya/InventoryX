using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Infrastructure.Persistence
{
    public class BaseRepository<TEntity> (AppDbContext context): IBaseRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context = context;
        public async Task<int> Add(TEntity entity)
        { 
            var nameProperty = typeof(TEntity).GetProperty("Name") ?? throw new InvalidOperationException("Name property does not exist.");
            var nameValue = nameProperty.GetValue(entity)?.ToString(); 
            var recordExists = await _context.Set<TEntity>().AnyAsync(e => EF.Property<string>(e, "Name") == nameValue);
            if (recordExists)
            {
                throw new InvalidOperationException("Record already exists.");
            }
             
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            var idProperty = typeof(TEntity).GetProperty("Id") ?? throw new InvalidOperationException("Id property does not exist.");
            var idValue = (int)idProperty.GetValue(entity);

            return idValue;
        }

        public async Task<int> Delete(int id)
        { 
            var entityToDelete = await _context.Set<TEntity>().FindAsync(id) ?? throw new InvalidOperationException("Record does not exist");
            _context.Set<TEntity>().Remove(entityToDelete);
             
            return await _context.SaveChangesAsync();
        }

        public async Task<TEntity> Get(int id) => await _context.FindAsync<TEntity>(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _context.Set<TEntity>().ToListAsync();
        public async Task<int> Update(TEntity entity)
        {
            var idProperty = typeof(TEntity).GetProperty("Id") ?? throw new InvalidOperationException("Id property does not exist.");
            var idValue = idProperty.GetValue(entity);
            var entityToUpdate = await _context.Set<TEntity>().FindAsync(idValue) ?? throw new InvalidOperationException("Record does not exist.");
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
             
            return await _context.SaveChangesAsync();
        }
    }
}
