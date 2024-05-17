using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Domain.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Type { get; set; }
        public byte[]? Image { get; set; }
    }
}
