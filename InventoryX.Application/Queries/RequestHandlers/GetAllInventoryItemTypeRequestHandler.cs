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
    public class GetAllInventoryItemTypeRequestHandler(IInventoryTypeService service, IMapper mapper) : IRequestHandler<GetAllInventoryItemTypeRequest, ApiResponse>
    {
        private readonly IInventoryTypeService _service = service;
        private readonly IMapper _mapper = mapper;
        public async Task<ApiResponse> Handle(GetAllInventoryItemTypeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _service.GetAllInventoryItemTypes() ?? throw new Exception("Failed to retrieve all inventory item types");
                var InventoryItemTypeDtos = _mapper.Map<IEnumerable<GetInventoryTypeDto>>(response);
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
