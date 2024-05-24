using InventoryX.Application.DTOs.Purchases; 
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Commands.Requests.Purchases
{
    public class CreatePurchaseCommand : IRequest<ApiResponse>
    {
        public required PurchaseCommandDto NewPurchaseDto { get; set; }
    }
}
