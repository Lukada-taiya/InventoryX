using InventoryX.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.DTOs.InventoryItemTypes
{
    public class GetInventoryItemTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<InventoryItem>? InventoryItems { get; set; }
    }
}
