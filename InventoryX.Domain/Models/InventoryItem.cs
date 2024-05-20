using InventoryX.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Domain.Models
{
    public class InventoryItem : BaseModel
    { 
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public virtual InventoryItemType Type { get; set; }
        public byte[]? Image { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public required decimal Price { get; set; }
        [Column(TypeName = "decimal(17,3)")]
        public decimal TotalAmount { get; set; }
        public virtual ICollection<Purchase>? Purchases { get; set; }
        public virtual ICollection<Sale>? Sales { get; set; }
    }
}
