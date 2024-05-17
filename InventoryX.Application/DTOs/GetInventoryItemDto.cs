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
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Type { get; set; }
        public byte[]? Image { get; set; }
    }
}
