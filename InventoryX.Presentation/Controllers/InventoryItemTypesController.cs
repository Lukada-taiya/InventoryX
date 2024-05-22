using InventoryX.Application.Commands.Requests;
using InventoryX.Application.DTOs;
using InventoryX.Application.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryX.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryItemTypesController(IMediator mediator) : Controller
    { 
        private readonly IMediator _mediator = mediator;

        [HttpGet("id")]
        public async Task<ActionResult> Get(int id)
        {
            var response = await _mediator.Send(new GetInventoryItemTypeRequest { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet] 
        public async Task<ActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllInventoryItemTypeRequest());
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<ActionResult> Add(InventoryTypeCommandDto inventoryItemType)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new CreateInventoryTypeCommand { NewInventoryItemTypeDto = inventoryItemType });
                return response.Success ? Ok(response) : BadRequest(response);
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Update(int id, InventoryTypeCommandDto inventoryItem)
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
            var response = await _mediator.Send(new DeleteInventoryItemTypeCommand { Id = id });
            return response.Success ? Ok(response) : BadRequest(response);
        }

    }
}
