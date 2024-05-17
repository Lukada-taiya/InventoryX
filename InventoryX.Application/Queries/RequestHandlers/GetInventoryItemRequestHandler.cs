using InventoryX.Application.Services;
using InventoryX.Domain.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Queries.Requests
{
    public class GetInventoryItemRequestHandler(IInventoryItemService service) : IRequestHandler<GetInventoryItemRequest, ApiResponse>
    { 
        private readonly IInventoryItemService _service = service;
        public async Task<ApiResponse> Handle(GetInventoryItemRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _service.GetInventoryItem(request.Id);
                if (response is null)
                {
                    throw new Exception("Inventory Item does not exist");
                }
                    return new ApiResponse
                    {
                        Success = true,
                        Message = "Retrieved inventory items successfully",
                        Body = response
                    };
            }catch(Exception ex)
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
