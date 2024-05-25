using AutoMapper;
using InventoryX.Application.DTOs.InventoryItems;
using InventoryX.Application.DTOs.InventoryItemTypes;
using InventoryX.Application.DTOs.Purchases;
using InventoryX.Application.DTOs.Users;
using InventoryX.Domain.Models; 

namespace InventoryX.Presentation.Configuration
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<InventoryItemCommandDto, InventoryItem>().ReverseMap();
            CreateMap<InventoryItemTypeCommandDto, InventoryItemType>().ReverseMap();
            CreateMap<PurchaseCommandDto,Purchase>().ReverseMap();
            CreateMap<GetInventoryItemTypeDto, InventoryItemType>().ReverseMap();
            CreateMap<GetInventoryItemDto, InventoryItem>().ReverseMap();
            CreateMap<GetPurchaseDto, Purchase>().ReverseMap();
            CreateMap<GetUserDto, User>().ReverseMap();
        }
    }
}
