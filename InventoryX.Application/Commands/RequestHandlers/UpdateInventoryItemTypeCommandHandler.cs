using AutoMapper;
using InventoryX.Application.Commands.Requests;
using InventoryX.Application.Services.Common;
using InventoryX.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Commands.RequestHandlers
{
    public class UpdateInventoryItemTypeCommandHandler(IInventoryTypeService service, IMapper mapper) : IRequestHandler<UpdateInventoryItemTypeCommand,ApiResponse>
    {
        private readonly IInventoryTypeService _service = service;
        private readonly IMapper _mapper = mapper; 

        public async Task<ApiResponse> Handle(UpdateInventoryItemTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var InventoryItemTypeEntity = _mapper.Map<InventoryItemType>(request.InventoryItemTypeDto);
                InventoryItemTypeEntity.Id = request.Id;
                InventoryItemTypeEntity.Updated_At = DateTime.UtcNow;
                var response = await _service.UpdateInventoryItemType(InventoryItemTypeEntity);
                if (response > 0)
                {
                    return new()
                    {
                        Id = InventoryItemTypeEntity.Id,
                        Success = true,
                        Message = "Inventory Item Type has been updated successfully"
                    };
                }
                throw new Exception("Failed to update Inventory Item Type");
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
