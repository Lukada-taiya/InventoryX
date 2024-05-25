using AutoMapper;
using InventoryX.Application.DTOs.InventoryItemTypes;
using InventoryX.Application.Queries.Requests.InventoryItemTypes;
using InventoryX.Application.Services.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Queries.RequestHandlers.InventoryItemTypes
{
    public class GetInventoryItemTypeRequestHandler(IInventoryItemTypeService service, IMapper mapper) : IRequestHandler<GetInventoryItemTypeRequest, ApiResponse>
    {
        private readonly IInventoryItemTypeService _service = service;
        private readonly IMapper _mapper = mapper;
        public async Task<ApiResponse> Handle(GetInventoryItemTypeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _service.GetInventoryItemType(request.Id) ?? throw new Exception("Inventory Item Type does not exist");
                var InventoryItemTypeDto = _mapper.Map<GetInventoryItemTypeDto>(response);
                return new ApiResponse
                {
                    Success = true,
                    Message = "Retrieved inventory item types successfully",
                    Body = InventoryItemTypeDto
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
