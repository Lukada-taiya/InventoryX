using InventoryX.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Domain.Models
{
    public class InventoryItemType : BaseModel
    {
        public required string Name { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
    }
}
