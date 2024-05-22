﻿using InventoryX.Application.Services.Common;
using InventoryX.Domain.Models;
using InventoryX.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Services
{
    public class InventoryTypeService(IBaseRepository<InventoryItemType> baseRepository) : IInventoryTypeService
    {
        private readonly IBaseRepository<InventoryItemType> _repository = baseRepository;
        public Task<int> AddInventoryItemType(InventoryItemType entity)
        {
            return _repository.Add(entity); 
        }

        public Task<int> DeleteInventoryItemType(int id)
        {
            return _repository.Delete(id);
        }

        public Task<IEnumerable<InventoryItemType>> GetAllInventoryItemTypes()
        {
            return _repository.GetAllAsync();
        }

        public Task<InventoryItemType> GetInventoryItemType(int id)
        {
            return _repository.Get(id);
        }

        public Task<int> UpdateInventoryItemType(InventoryItemType entity)
        {
            return _repository.Update(entity);
        }
    }
}
