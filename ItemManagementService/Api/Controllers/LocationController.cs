using ItemManagementService.Application.Commands.Locations;
using ItemManagementService.Application.Queries.Items;
using ItemManagementService.Application.Queries.Locations;
using ItemManagementService.Infrastructure.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Location;

namespace ItemManagementService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController: ControllerBase
{
    private readonly IMediator _mediator;

    public LocationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<LocationDto>> GetById(Guid id)
    {
        var location = await _mediator.Send(new GetLocationByIdQuery(id));
        return location != null ? Ok(location) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<LocationDto>> Create([FromBody] CreateLocationCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = id }, null);
    }
    [HttpPut("{id:guid}/name")]
    public async Task<IActionResult> UpdateName(Guid id, [FromBody] string newName)
    {
        var success = await _mediator.Send(new RenameLocationCommand(id, newName));
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _mediator.Send(new DeleteLocationCommand(id));
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _mediator.Send(new GetAllLocationQuery());
        return Ok(items);
    }
}