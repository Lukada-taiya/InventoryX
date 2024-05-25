using InventoryX.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.DTOs.Purchases
{
    public class SaleCommandDto
    {
        [Required]
        public int InventoryItemId { get; set; }  
        [Required]
        public required decimal Quantity { get; set; } 
        [Required]
        public required decimal Price { get; set; } 
    }
}
