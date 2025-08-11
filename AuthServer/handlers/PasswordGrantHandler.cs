using System.Security.Claims;
using AuthService.Dtos;
using AuthService.Services;
using OpenIddict.Abstractions;
using OpenIddict.Server;
using OpenIddict.Server.AspNetCore;
using Shared.Domain.ApplicationUser;

namespace AuthService.handlers;

public class PasswordGrantHandler : IOpenIddictServerHandler<OpenIddictServerEvents.HandleTokenRequestContext>
{
    private readonly IAuthService _authService;

    public PasswordGrantHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async ValueTask HandleAsync(OpenIddictServerEvents.HandleTokenRequestContext context)
    {
        if (context.Request.GrantType != OpenIddictConstants.GrantTypes.Password)
            return;

        var email = context.Request.Username;
        var password = context.Request.Password;
        var userEntity = await _authService.ValidateUserAsync(new LoginDto
        {
            Email = email!,
            Password = password!
        }, context.CancellationToken);

        if (userEntity == null)
        {
            context.Reject(
                OpenIddictConstants.Errors.InvalidGrant,
                "Invalid email or password.");
            return;
        }

        var user = new ApplicationUser(userEntity);

        var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        var claims = await _authService.GenerateClaimsAsync(user, context.CancellationToken);
        foreach (var claim in claims) identity.AddClaim(claim);
        var principal = new ClaimsPrincipal(identity);

        principal.SetScopes(context.Request.GetScopes());
        principal.SetResources("resource_server");

        context.SignIn(principal);
    }
}