using AuthService.Dtos;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain.ApplicationUser.Repositories;

namespace AuthService.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly Services.AuthService _authService;

    public AuthController(Services.AuthService authServiceJ, IApplicationUserRepository userRepository)
    {
        _authService = authServiceJ;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var result = await _authService.RegisterUserAsync(dto);
        if (!result.Success)
            return BadRequest(result.Errors);
        return Ok();
    }
}