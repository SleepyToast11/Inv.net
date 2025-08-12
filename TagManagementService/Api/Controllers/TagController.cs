using MediatR;
using Microsoft.AspNetCore.Mvc;
using TagManagementService.Application.Command;
using TagManagementService.Application.Queries;

namespace TagManagementService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateTag(CreateTagCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _mediator.Send(new GetTagByIdQuery(id));
        if (item == null) return NotFound();
        return Ok(item);
    }
    
    [HttpPut("{id}/rename")]
    public async Task<IActionResult> Rename(Guid id, [FromBody] string newName)
    {
        var result = await _mediator.Send(new RenameTagCommand(id, newName));
        if (!result)
            return NotFound();
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteTagCommand(id));
        if (result) 
            return Ok();
        return NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _mediator.Send(new GetAllTagsQuery());
        return Ok(items);
    }
    
}