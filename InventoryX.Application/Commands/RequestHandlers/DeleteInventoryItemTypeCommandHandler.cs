using AutoMapper;
using InventoryX.Application.Commands.Requests;
using InventoryX.Application.Services.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Commands.RequestHandlers
{
    public class DeleteInventoryItemTypeCommandHandler(IInventoryTypeService service, IMapper mapper) : IRequestHandler<DeleteInventoryItemTypeCommand, ApiResponse>
    {
        private readonly IInventoryTypeService _service = service; 

        public async Task<ApiResponse> Handle(DeleteInventoryItemTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _service.DeleteInventoryItemType(request.Id);
                if (response > 0)
                {
                    return new()
                    {
                        Success = true,
                        Message = "Inventory Item Type has been deleted successfully"
                    };
                }
                throw new Exception("Failed to delete Inventory Item Type");
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
