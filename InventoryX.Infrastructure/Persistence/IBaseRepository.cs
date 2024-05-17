﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Infrastructure.Persistence
{
    public interface IBaseRepository<TEntity>
    {
        Task<int> Add(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> Get(int id);
        Task<int> Update(TEntity entity);
        Task<int> Delete(int id);
    }
}