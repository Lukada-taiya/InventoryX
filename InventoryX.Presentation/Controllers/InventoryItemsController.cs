using InventoryX.Application.Commands.Requests.InventoryItems;
using InventoryX.Application.Commands.Requests.InventoryItemTypes;
using InventoryX.Application.DTOs.InventoryItems;
using InventoryX.Application.DTOs.InventoryItemTypes; 
using InventoryX.Application.Queries.Requests.InventoryItems;
using MediatR;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;

namespace InventoryX.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryItemsController(IMediator mediator) : Controller
    { 
        private readonly IMediator _mediator = mediator;

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var response = await _mediator.Send(new GetInventoryItemRequest { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet] 
        public async Task<ActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllInventoryItemRequest());
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<ActionResult> Add(InventoryItemCommandDto inventoryItem)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new CreateInventoryItemCommand { NewInventoryItemDto = inventoryItem });
                return response.Success ? Ok(response) : BadRequest(response);
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Update(int id, InventoryItemTypeCommandDto inventoryItem)
        {
            if(ModelState.IsValid)
            {
                var response = await _mediator.Send(new UpdateInventoryItemTypeCommand { Id = id, InventoryItemTypeDto = inventoryItem });
                return response.Success ? Ok(response) : BadRequest(response);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteInventoryItemCommand { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }

    }
}
