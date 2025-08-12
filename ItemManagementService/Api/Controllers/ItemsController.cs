using ItemManagementService.Application.Commands.Items;
using ItemManagementService.Application.Queries.Items;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ItemManagementService.Api.Controllers;

[ApiController]
[Route("api/items")]
public class ItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateItemCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id:guid}/name")]
    public async Task<IActionResult> UpdateName(Guid id, [FromBody] string newName)
    {
        var success = await _mediator.Send(new UpdateItemNameCommand(id, newName));
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _mediator.Send(new DeleteItemCommand(id));
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _mediator.Send(new GetItemByIdQuery(id));
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _mediator.Send(new GetAllItemsQuery());
        return Ok(items);
    }
    
    // Locations
    [HttpPost("{id:guid}/locations")]
    public async Task<IActionResult> AddLocation(Guid id, [FromBody] AddItemLocationCommand command)
    {
        if (id != command.ItemId) return BadRequest("Mismatched ItemId");
        var success = await _mediator.Send(command);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpPut("{id:guid}/locations/{locationId:guid}")]
    public async Task<IActionResult> UpdateLocationQuantity(Guid id, Guid locationId, [FromBody] int newQuantity)
    {
        var command = new UpdateItemLocationQuantityCommand(id, locationId, newQuantity);
        var success = await _mediator.Send(command);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:guid}/locations/{locationId:guid}")]
    public async Task<IActionResult> RemoveLocation(Guid id, Guid locationId)
    {
        var success = await _mediator.Send(new RemoveItemLocationCommand(id, locationId));
        if (!success) return NotFound();
        return NoContent();
    }
    
    // Tags
    [HttpPost("{id:guid}/tags")]
    public async Task<IActionResult> AddTag(Guid id, [FromBody] Guid tagId)
    {
        var success = await _mediator.Send(new AddItemTagCommand(id, tagId));
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:guid}/tags/{tagId:guid}")]
    public async Task<IActionResult> RemoveTag(Guid id, Guid tagId)
    {
        var success = await _mediator.Send(new RemoveItemTagCommand(id, tagId));
        if (!success) return NotFound();
        return NoContent();
    }
}
