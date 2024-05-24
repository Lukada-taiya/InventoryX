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
    public class PurchaseService(IPurchaseRepository repository, IAuthService authService) : IPurchaseService
    {
        private readonly IPurchaseRepository _repository = repository;
        private readonly IAuthService _authService = authService;
        public async Task<int> AddPurchase(Purchase entity)
        {
            var user = await _authService.GetAuthenticatedUser();
            entity.UserId = user.Id;
            return await _repository.Add(entity);
        }

        public Task<int> DeletePurchase(int id)
        {
            return _repository.Delete(id);
        }

        public Task<IEnumerable<Purchase>> GetAllPurchases()
        {
            return _repository.GetAllAsync(
                    i => i.InventoryItem,
                    i => i.Purchaser
                );
        }

        public Task<Purchase> GetPurchase(int id)
        {
            return _repository.Get(
                id,
                 i => i.InventoryItem,
                 i => i.Purchaser
                );
        }

        public Task<int> UpdatePurchase(Purchase entity)
        {
            return _repository.Update(entity);
        }
    }
}
