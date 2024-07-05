using InventoryX.Application.Commands.RequestHandlers.RetailStocks;
using InventoryX.Application.Commands.Requests.RetailStock; 
using InventoryX.Application.DTOs.RetailStock; 
using InventoryX.Application.Queries.Requests.RetailStock; 
using InventoryX.Domain.Models;
using InventoryX.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryX.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetailStockController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var response = await _mediator.Send(new GetRetailStockRequest { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpGet]
        [Route("GetByInventoryItem/{id}")]
        public async Task<ActionResult> GetByInventoryItem(int id)
        {
            var response = await _mediator.Send(new GetByInventoryItemRetailStockRequest { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllRetailStockRequest());
            return response.Success ? Ok(response) : BadRequest(response);
        } 

        [HttpPut] 
        public async Task<ActionResult> Update(RetailStockCommandDto RetailStock)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new UpdateRetailStockCommand { RetailStock = RetailStock });
                return response.Success ? Ok(response) : BadRequest(response);
            }
            return BadRequest(ModelState);
        }
    }
}
