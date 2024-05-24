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

namespace InventoryX.Application.Commands.RequestHandlers.InventoryItems
{
    public class CreateInventoryItemCommandHandler(IInventoryItemService service, IMapper mapper) : IRequestHandler<CreateInventoryItemCommand, ApiResponse>
    {
        private readonly IInventoryItemService _service = service;
        private readonly IMapper _mapper = mapper;
        public async Task<ApiResponse> Handle(CreateInventoryItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var InventoryItemEntity = _mapper.Map<InventoryItem>(request.NewInventoryItemDto);
                InventoryItemEntity.Created_At = DateTime.UtcNow;
                var response = await _service.AddInventoryItem(InventoryItemEntity);
                if (response > 0)
                {
                    return new()
                    {
                        Id = response,
                        Success = true,
                        Message = "Inventory Item has been created successfully"
                    };
                }
                throw new Exception("Failed to create Inventory Item");
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
