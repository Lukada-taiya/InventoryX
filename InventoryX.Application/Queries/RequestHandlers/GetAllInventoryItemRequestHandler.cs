using InventoryX.Application.Queries.Requests;
using InventoryX.Application.Services; 
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Queries.RequestHandlers
{
    public class GetAllInventoryItemRequestHandler(IInventoryItemService service) : IRequestHandler<GetAllInventoryItemRequest, ApiResponse>
    {
        private readonly IInventoryItemService _service = service;
        public async Task<ApiResponse> Handle(GetAllInventoryItemRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _service.GetAllInventoryItems() ?? throw new Exception("Failed to retrieve all inventory items");
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
