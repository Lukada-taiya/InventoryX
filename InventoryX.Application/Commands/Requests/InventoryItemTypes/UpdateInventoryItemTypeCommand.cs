using InventoryX.Application.DTOs.InventoryItemTypes;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Commands.Requests.InventoryItemTypes
{
    public class UpdateInventoryItemTypeCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public required InventoryItemTypeCommandDto InventoryItemTypeDto { get; set; }
    }
}
