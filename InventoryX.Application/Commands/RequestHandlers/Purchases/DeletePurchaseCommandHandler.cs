using AutoMapper;
using InventoryX.Application.Commands.Requests.Purchases;
using InventoryX.Application.Services.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Commands.RequestHandlers.Purchases
{
    public class DeletePurchaseCommandHandler(IPurchaseService service, IMapper mapper) : IRequestHandler<DeletePurchaseCommand, ApiResponse>
    {
        private readonly IPurchaseService _service = service;
        private readonly IMapper _mapper = mapper;
        public async Task<ApiResponse> Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _service.DeletePurchase(request.Id);
                if (response > 0)
                {
                    return new()
                    {
                        Success = true,
                        Message = "Purchase has been deleted successfully"
                    };
                }
                throw new Exception("Failed to delete Purchase");
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
