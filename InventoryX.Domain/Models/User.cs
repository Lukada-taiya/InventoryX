using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Domain.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<Purchase>? Purchases { get; set; }
        public virtual ICollection<Sale>? Sales { get; set; }
    }
}
