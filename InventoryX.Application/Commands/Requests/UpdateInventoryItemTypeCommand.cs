﻿using InventoryX.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Commands.Requests
{
    public class UpdateInventoryItemTypeCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; } 
        public required InventoryTypeCommandDto InventoryItemTypeDto { get; set; }
    }
}