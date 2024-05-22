using InventoryX.Domain.Models;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.DTOs
{
    public class GetInventoryItemDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string? Description { get; set; }
        public GetInventoryTypeDto Type { get; set; }
        public byte[]? Image { get; set; }
        public decimal Price { get; set; } 
        public decimal TotalAmount { get; set; }
        public ICollection<Purchase>? Purchases { get; set; }
        public ICollection<Sale>? Sales { get; set; }
    }
}
