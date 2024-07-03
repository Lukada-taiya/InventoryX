using InventoryX.Application.DTOs.InventoryItems;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Commands.Requests.InventoryItems
{
    public class CreateInventoryItemCommand : IRequest<ApiResponse>
    {
        public required InventoryItemCommandDto NewInventoryItemDto { get; set; }
        public decimal RetailQuantity {  get; set; }
    }
}
