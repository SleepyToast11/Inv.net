using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Persistence.Entities.ApplicationUser;
using UserManagementService.Application.Command;
using UserManagementService.Application.Query;

namespace UserManagementService.Api.Controllers;
//
[ApiController]
[Route("api/[controller]")]
public class UserManagementController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        if (user is null)
            return NotFound();
        return Ok(user);
    }

    [HttpGet("tenants/{id:guid}")]
    public async Task<IActionResult> GetByTenantId(Guid id)
    {
        var user = await _mediator.Send(new GetAllUsersByTenantQuery(id));
        return Ok(user);
    }

    [HttpPost("{userId:guid}/{tenantId:guid}")]
    public async Task<IActionResult> AddTenantToUser(Guid userId, Guid tenantId)
    {
        var id = await _mediator.Send(new AddUserPermissionTenantCommand(userId, tenantId));
        if  (id is null)
            return NotFound();
        return CreatedAtAction(nameof(GetById), new { id = id }, null);
    }
    
    [HttpPost("{userId:guid}/{tenantId:guid}/{scopeId:maxlength(20)}")]
    public async Task<IActionResult> AddScopeToPermission(
        Guid userId,
        Guid tenantId,
        string scopeId,
        [FromBody] PermissionLevel level)
    {
        var id = await _mediator.Send(new AddUserPermissionScopeCommand(userId, tenantId, scopeId, level));
        if  (id is null)
            return NotFound();
        return CreatedAtAction(nameof(GetById), new { id = id }, null);
    }
    
    [HttpPut("{userId:guid}/{tenantId:guid}/{scopeId:maxlength(20)}")]
    public async Task<IActionResult> UpdateTenantToUser(Guid userId, Guid tenantId, string scopeId, [FromBody] PermissionLevel level)
    {
        var id = await _mediator.Send(new UpdateUserPermissionScopeCommand(userId, tenantId, scopeId, level));
        if (id is null)
            return NotFound();
        return CreatedAtAction(nameof(GetById), new { id = id }, null);
    }

    [HttpDelete("{userId:guid}/{tenantId:guid}/{scopeId:maxlength(20)}")]
    public async Task<IActionResult> DeleteScopeFromUser(Guid userId, Guid tenantId, string scopeId)
    {
        var deleted = await _mediator.Send(new DeleteUserPermissionScope(userId, tenantId, scopeId));
        if (!deleted)
            return NotFound();
        return Ok();
    }

    [HttpDelete("{userId:guid}/{tenantId:guid}")]
    public async Task<IActionResult> DeleteTenantFromUser(Guid userId, Guid tenantId)
    {
        var deleted = await _mediator.Send(new DeleteUserPermissionTenantCommand(userId, tenantId));
        if (!deleted)
            return NotFound();
        return Ok();
    }
}