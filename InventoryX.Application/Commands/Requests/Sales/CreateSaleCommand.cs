﻿using InventoryX.Application.DTOs.Sales;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Commands.Requests.Sales
{
    public class CreateSaleCommand : IRequest<ApiResponse>
    {
        public required SaleCommandDto NewSaleDto { get; set; }
    }
}
