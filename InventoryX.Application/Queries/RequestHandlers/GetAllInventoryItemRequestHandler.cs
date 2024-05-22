using AutoMapper;
using InventoryX.Application.DTOs;
using InventoryX.Application.Queries.Requests;
using InventoryX.Application.Services.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Queries.RequestHandlers
{
    public class GetAllInventoryItemRequestHandler(IInventoryItemService service, IMapper mapper) : IRequestHandler<GetAllInventoryItemRequest, ApiResponse>
    {
        private readonly IInventoryItemService _service = service;
        private readonly IMapper _mapper = mapper;
        public async Task<ApiResponse> Handle(GetAllInventoryItemRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _service.GetAllInventoryItems() ?? throw new Exception("Failed to retrieve all inventory items");
                var InventoryItemDtos = _mapper.Map<IEnumerable<GetInventoryItemDto>>(response);
                return new()
                {
                    Success = true,
                    Message = "Retrieved all inventory items successfully",
                    Body = response
                };
            }catch(Exception ex)
            {
                return new()
                {
                    Success = false,
                    Message = ex.Message ?? "Something went wrong. Try again later.",
                };
            }
        }
    }
}
