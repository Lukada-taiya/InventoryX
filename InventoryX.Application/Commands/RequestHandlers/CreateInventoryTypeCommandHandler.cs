﻿using AutoMapper;
using InventoryX.Application.Commands.Requests;
using InventoryX.Application.Services.Common;
using InventoryX.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Commands.RequestHandlers
{
    public class CreateInventoryTypeCommandHandler(IInventoryTypeService service, IMapper mapper) : IRequestHandler<CreateInventoryTypeCommand, ApiResponse>
    {
        private readonly IInventoryTypeService _service= service;
        private readonly IMapper _mapper = mapper;        
        public async Task<ApiResponse> Handle(CreateInventoryTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var InventoryItemTypeEntity = _mapper.Map<InventoryItemType>(request.NewInventoryItemTypeDto);
                InventoryItemTypeEntity.Created_At = DateTime.UtcNow;
                var response = await _service.AddInventoryItemType(InventoryItemTypeEntity);
                if (response > 0)
                {
                    return new()
                    {
                        Id = response,
                        Success = true,
                        Message = "Inventory Item has been created successfully"
                    };
                }
                throw new Exception("Failed to create Inventory Item");
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