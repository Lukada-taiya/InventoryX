﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Queries.Requests.InventoryItemTypes
{
    public class GetInventoryItemTypeRequest : IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}
