﻿using InventoryX.Domain.Data;
using InventoryX.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Services
{
    public class InventoryItemService(IBaseRepository<InventoryItem> repository) : IInventoryItemService
    {
        private readonly IBaseRepository<InventoryItem> _repository = repository;
        public Task<int> AddInventoryItem(InventoryItem entity)
        {
            return _repository.Add(entity);
        }

        public Task<int> DeleteInventoryItem(int id)
        {
            return _repository.Delete(id);
        }

        public Task<IEnumerable<InventoryItem>> GetAllInventoryItems()
        {
            return _repository.GetAllAsync();
        }

        public Task<InventoryItem> GetInventoryItem(int id)
        {
            return _repository.Get(id);
        }

        public Task<int> UpdateInventoryItem(InventoryItem entity)
        {
            return _repository.Update(entity);
        }
    }
}