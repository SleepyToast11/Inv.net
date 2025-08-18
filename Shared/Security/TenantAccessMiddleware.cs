using System.Text.Json;
using Shared.Dtos;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Shared.Security;

public class TenantAccessMiddleware
{
    private readonly RequestDelegate _next;

    public TenantAccessMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var access = context.RequestServices.GetRequiredService<CurrentTenantAccess>();

        var claims = context.User.FindAll("tenant_permissions"); // JWT claim key
        foreach (var claim in claims)
        {
            var permissionClaim = JsonSerializer.Deserialize<TenantPermissionClaim>(claim.Value);
            if (permissionClaim is null) continue;

            foreach (var kvp in permissionClaim.Permissions)
            {
                var scope = kvp.Key;
                var level = int.Parse(kvp.Value);

                if (level >= 1)
                    access.AddReadable(scope, permissionClaim.TenantId);

                if (level >= 2)
                    access.AddWritable(scope, permissionClaim.TenantId);

                if (level >= 3)
                    access.AddAdmin(scope, permissionClaim.TenantId);
            }
        }

        var superAdminClaim = context.User.FindFirst("superAdmin");
        if (superAdminClaim != null && superAdminClaim.Value.Equals("true", StringComparison.InvariantCultureIgnoreCase))
            access.SuperAdmin = true;
        else
            access.SuperAdmin = false;
        
        await _next(context);
    }
}
