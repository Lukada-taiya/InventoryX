using InventoryX.Application.DTOs.InventoryItemTypes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Commands.Requests.InventoryItemTypes
{
    public class CreateInventoryTypeCommand : IRequest<ApiResponse>
    {
        public required InventoryItemTypeCommandDto NewInventoryItemTypeDto { get; set; }
    }
}
