using MediatR;
using Microsoft.AspNetCore.Mvc;
using TagManagementService.Api.Handlers.SuperTag;
using TagManagementService.Application.Command;
using TagManagementService.Application.Queries;

namespace TagManagementService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuperTagController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateSuperTag([FromBody] CreateSuperTagCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = id }, null);
    }

    [HttpPut("{id}/rename")]
    public async Task<IActionResult> RenameTag(Guid id, [FromBody] string newName)
    {
        var result = await _mediator.Send(new RenameSuperTagCommand(id, newName));
         if (!result)
             return NotFound();
         return Ok();
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var superTag = await _mediator.Send(new GetSuperTagByIdQuery(id));
        if (superTag == null)
            return NotFound();
        
        return Ok(superTag);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        var result = await _mediator.Send(new DeleteSuperTagCommand(id));
        if (result) 
            return Ok();
        return NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var superTags = _mediator.Send(new GetAllSuperTagsQuery());
        return Ok(superTags);
    }
}