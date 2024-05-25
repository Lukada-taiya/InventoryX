using AutoMapper; 
using InventoryX.Application.Commands.Requests.Purchases; 
using InventoryX.Application.Services.IServices;
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
                var PurchaseEntity = _mapper.Map<Purchase>(request.PurchaseDto);
                PurchaseEntity.Id = request.Id;
                PurchaseEntity.Updated_At = DateTime.UtcNow;
                var response = await _service.UpdatePurchase(PurchaseEntity);
                if (response > 0)
                {
                    return new()
                    {
                        Id = PurchaseEntity.Id,
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
