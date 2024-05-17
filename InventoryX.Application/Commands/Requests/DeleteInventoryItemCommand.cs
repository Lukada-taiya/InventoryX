using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Application.Commands.Requests
{
    public class DeleteInventoryItemCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}
