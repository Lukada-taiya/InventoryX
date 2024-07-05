using AutoMapper;
using InventoryX.Application.Commands.Requests.RetailStock;
using InventoryX.Application.Services.IServices;
using InventoryX.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Commands.RequestHandlers.RetailStocks
{
    public class UpdateRetailStockCommandHandler(IRetailStockService service, IInventoryItemService inventoryItemService, IMapper mapper) : IRequestHandler<UpdateRetailStockCommand, ApiResponse>
    {
        private readonly IRetailStockService _service = service;
        private readonly IInventoryItemService _inventoryItemService = inventoryItemService;
        private readonly IMapper _mapper = mapper;
        public async Task<ApiResponse> Handle(UpdateRetailStockCommand request, CancellationToken cancellationToken)
        {
            try
            {
                RetailStock updatedRetailStock = _mapper.Map<RetailStock>(request.RetailStock);
                InventoryItem inventoryItem = await _inventoryItemService.GetInventoryItem(updatedRetailStock.InventoryItemId) ?? throw new Exception("Inventory Item does not exist");
                RetailStock oldRetailStock = await _service.GetRetailStock("InventoryItemId", request.RetailStock.InventoryItemId) ?? throw new Exception("Retail Stock does not exist"); 
                if (updatedRetailStock.Quantity > inventoryItem.TotalAmount) throw new Exception("Retail Stock quantity cannot be greater than total inventory item amount");
                updatedRetailStock.Id = oldRetailStock.Id;
                updatedRetailStock.Updated_At = DateTime.UtcNow; 
                var response = await _service.UpdateRetailStock(updatedRetailStock);
                if (response <= 0) throw new Exception("Failed to update Retail Stock");

                return new() { Id = updatedRetailStock.Id, Message = "Retail Stock updated successfully.", Success = true };

            }
            catch (Exception e)
            {
                return new()
                {
                    Success = false,
                    Message = e.Message ?? "Something went wrong. Try again later."
                };
            }
        }
    }
}
