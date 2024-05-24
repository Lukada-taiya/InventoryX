using AutoMapper;
using InventoryX.Application.DTOs.Purchases;
using InventoryX.Application.Queries.Requests.Purchases;
using InventoryX.Application.Services.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Queries.RequestHandlers.Purchases
{
    public class GetPurchaseRequestHandler(IPurchaseService service, IMapper mapper) : IRequestHandler<GetPurchaseRequest, ApiResponse>
    {
        private readonly IPurchaseService _service = service;
        private readonly IMapper _mapper = mapper;
        public async Task<ApiResponse> Handle(GetPurchaseRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _service.GetPurchase(request.Id) ?? throw new Exception("Purchase does not exist");
                var PurchaseDto = _mapper.Map<GetPurchasesDto>(response);
                return new ApiResponse
                {
                    Success = true,
                    Message = "Retrieved Purchase successfully",
                    Body = PurchaseDto
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
