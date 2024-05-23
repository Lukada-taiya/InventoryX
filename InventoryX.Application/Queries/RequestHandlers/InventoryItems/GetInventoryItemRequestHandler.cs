using AutoMapper;
using InventoryX.Application.DTOs.InventoryItems;
using InventoryX.Application.Queries.Requests.InventoryItems;
using InventoryX.Application.Services.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Queries.RequestHandlers.InventoryItems
{
    public class GetInventoryItemRequestHandler(IInventoryItemService service, IMapper mapper) : IRequestHandler<GetInventoryItemRequest, ApiResponse>
    {
        private readonly IInventoryItemService _service = service;
        private readonly IMapper _mapper = mapper;
        public async Task<ApiResponse> Handle(GetInventoryItemRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _service.GetInventoryItem(request.Id) ?? throw new Exception("Inventory Item does not exist");
                var InventoryItemDto = _mapper.Map<GetInventoryItemDto>(response);
                return new ApiResponse
                {
                    Success = true,
                    Message = "Retrieved inventory items successfully",
                    Body = InventoryItemDto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = ex.Message ?? "Something went wrong. Try again later."
                };

            }
        }
    }
}
