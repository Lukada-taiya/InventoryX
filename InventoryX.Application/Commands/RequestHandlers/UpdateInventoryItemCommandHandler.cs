using AutoMapper;
using InventoryX.Application.Commands.Requests;
using InventoryX.Application.Services;
using InventoryX.Domain.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Commands.RequestHandlers
{
    public class UpdateInventoryItemCommandHandler(IInventoryItemService service, IMapper mapper) : IRequestHandler<UpdateInventoryItemCommand,ApiResponse>
    {
        private readonly IInventoryItemService _service = service;
        private readonly IMapper _mapper = mapper; 

        public async Task<ApiResponse> Handle(UpdateInventoryItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var InventoryItemEntity = _mapper.Map<InventoryItem>(request.InventoryItemDto);
                InventoryItemEntity.Id = request.Id;
                var response = await _service.UpdateInventoryItem(InventoryItemEntity);
                if (response > 0)
                {
                    return new()
                    {
                        Id = InventoryItemEntity.Id,
                        Success = true,
                        Message = "Inventory Item has been updated successfully"
                    };
                }
                throw new Exception("Failed to update Inventory Item");
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
