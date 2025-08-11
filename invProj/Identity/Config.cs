using Duende.IdentityServer.Models;

namespace invProj.Identity;

public class Config
{
    private readonly IConfiguration _configuration;

    public Config(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(), // includes sub (user id)
        new IdentityResources.Profile(), // includes name, etc.
        new("tenant", new[] { "tenant_id" }) // custom claim
    ];

    public IEnumerable<ApiScope> ApiScopes =>
    [
        new("invproj.api", "Inventory API")
    ];

    public IEnumerable<Client> Clients =>
    [
        // Example for a SPA (JavaScript frontend)
        new()
        {
            ClientId = "spa-client",
            ClientName = "SPA Client",
            AllowedGrantTypes = GrantTypes.Code,
            RequirePkce = true,
            RequireClientSecret = false,

            RedirectUris = { _configuration["Auth:Spa:RedirectUri"] }, //"https://localhost:5173/callback"
            PostLogoutRedirectUris =
                { _configuration["Auth:Spa:PostLogoutRedirectUri"] }, //"https://localhost:5173/logout"
            AllowedCorsOrigins = { _configuration["Auth:Spa:Origin"] }, //"https://localhost:5173" 

            AllowedScopes =
            {
                "openid",
                "profile",
                "tenant",
                "invproj.api"
            },

            AllowAccessTokensViaBrowser = true
        },

        // Example for a server-side app (e.g., Swagger UI)
        new()
        {
            ClientId = "swagger-client",
            ClientSecrets = { new Secret(_configuration["IdentityServer:Clients:Swagger:ClientSecret"].Sha256()) },

            AllowedGrantTypes = GrantTypes.Code,

            RedirectUris = { "https://localhost:5001/swagger/oauth2-redirect.html" },
            AllowedScopes =
            {
                "openid",
                "profile",
                "tenant",
                "invproj.api"
            },
            RequirePkce = true
        }
    ];
}