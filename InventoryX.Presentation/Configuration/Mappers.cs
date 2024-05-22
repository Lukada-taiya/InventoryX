using AutoMapper;
using InventoryX.Application.DTOs; 
using InventoryX.Domain.Models;

namespace InventoryX.Presentation.Configuration
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<InventoryItemCommandDto, InventoryItem>().ReverseMap();
            CreateMap<InventoryTypeCommandDto, InventoryItemType>().ReverseMap();
            CreateMap<GetInventoryTypeDto, InventoryItemType>().ReverseMap();
            CreateMap<GetInventoryItemDto, InventoryItem>().ReverseMap();
        }
    }
}
