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
        }
    }
}
