using AutoMapper;
using InventoryX.Application.Commands.Requests.InventoryItems;
using InventoryX.Application.Commands.Requests.Purchases;
using InventoryX.Application.Services.Common;
using InventoryX.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Commands.RequestHandlers.Purchases
{
    public class UpdatePurchaseCommandHandler(IPurchaseService service, IMapper mapper) : IRequestHandler<UpdatePurchaseCommand, ApiResponse>
    {
        private readonly IPurchaseService _service = service;
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponse> Handle(UpdatePurchaseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var InventoryItemEntity = _mapper.Map<Purchase>(request.PurchaseDto);
                InventoryItemEntity.Id = request.Id;
                InventoryItemEntity.Updated_At = DateTime.UtcNow;
                var response = await _service.UpdatePurchase(InventoryItemEntity);
                if (response > 0)
                {
                    return new()
                    {
                        Id = InventoryItemEntity.Id,
                        Success = true,
                        Message = "Purchase has been updated successfully"
                    };
                }
                throw new Exception("Failed to update Purchase");
            }
            catch (Exception ex)
            {
                return new()
                {
                    Success = false,
                    Message = ex.Message ?? "Something went wrong. Try again later."
                };
            }
        }
    }
}
