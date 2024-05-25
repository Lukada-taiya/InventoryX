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
    public class GetAllInventoryItemTypeRequestHandler(IInventoryItemTypeService service, IMapper mapper) : IRequestHandler<GetAllInventoryItemTypeRequest, ApiResponse>
    {
        private readonly IInventoryItemTypeService _service = service;
        private readonly IMapper _mapper = mapper;
        public async Task<ApiResponse> Handle(GetAllInventoryItemTypeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _service.GetAllInventoryItemTypes() ?? throw new Exception("Failed to retrieve all inventory item types");
                var InventoryItemTypeDtos = _mapper.Map<IEnumerable<GetInventoryItemTypeDto>>(response);
                return new()
                {
                    Success = true,
                    Message = "Retrieved all inventory item types successfully",
                    Body = InventoryItemTypeDtos
                };
            }
            catch (Exception ex)
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
