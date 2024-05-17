using AutoMapper;
using InventoryX.Application.Commands.Requests;
using InventoryX.Application.Services; 
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Commands.RequestHandlers
{
    public class DeleteInventoryItemCommandHandler(IInventoryItemService service, IMapper mapper) : IRequestHandler<DeleteInventoryItemCommand, ApiResponse>
    {
        private readonly IInventoryItemService _service = service;
        private readonly IMapper _mapper = mapper;
        public async Task<ApiResponse> Handle(DeleteInventoryItemCommand request, CancellationToken cancellationToken)
        {
            try
            { 
                var response = await _service.DeleteInventoryItem(request.Id);
                if (response > 0)
                {
                    return new()
                    {
                        Success = true,
                        Message = "Inventory Item has been deleted successfully"
                    };
                }
                throw new Exception("Failed to delete Inventory Item");
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
