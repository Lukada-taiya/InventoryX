using InventoryX.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Services.Common
{
    public interface IAuthService
    {
        Task<User> GetAuthenticatedUser(); 
    }
}
