using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.DTOs
{
    public class InventoryItemCommandDto
    {
        [Required]
        public required string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public required string Type { get; set; }
        public byte[]? Image { get; set; }
    }
}
