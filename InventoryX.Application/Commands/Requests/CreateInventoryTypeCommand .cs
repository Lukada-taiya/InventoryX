using InventoryX.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Commands.Requests
{
    public class CreateInventoryTypeCommand : IRequest<ApiResponse>
    {
        public required InventoryTypeCommandDto NewInventoryItemTypeDto { get; set; }
    }
}
