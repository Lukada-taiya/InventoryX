using AutoMapper;
using InventoryX.Application.DTOs.InventoryItems;
using InventoryX.Application.DTOs.InventoryItemTypes;
using InventoryX.Domain.Models;

namespace InventoryX.Presentation.Configuration
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<InventoryItemCommandDto, InventoryItem>().ReverseMap();
            CreateMap<InventoryItemTypeCommandDto, InventoryItemType>().ReverseMap();
            CreateMap<GetInventoryItemTypeDto, InventoryItemType>().ReverseMap();
            CreateMap<GetInventoryItemDto, InventoryItem>().ReverseMap();
        }
    }
}
