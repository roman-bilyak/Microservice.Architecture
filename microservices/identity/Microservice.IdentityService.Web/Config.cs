using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;
using System.Text.Json;

namespace Microservice.IdentityService.Web;

public static class Config
{
    public static List<TestUser> TestUsers =>
        new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "88421122",
                Username = "bob",
                Password = "bob",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Bob Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Bob"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                    new Claim(JwtClaimTypes.Email, "bob.smith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.Address,
                        JsonSerializer.Serialize(new
                        {
                            street_address = "One Hacker Way",
                            locality = "Heidelberg",
                            postal_code = 69118,
                            country = "Germany"
                        }), IdentityServerConstants.ClaimValueTypes.Json),
                    new Claim(JwtClaimTypes.PhoneNumber, "+4930901820"),
                    new Claim(JwtClaimTypes.PhoneNumberVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.Role, "admin")
                }
            },
            new TestUser
            {
                SubjectId = "81872755",
                Username = "alice",
                Password = "alice",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Alice Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Alice"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                    new Claim(JwtClaimTypes.Email, "alice.smith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.Address,
                        JsonSerializer.Serialize(new
                        {
                            street_address = "New Avenue",
                            locality = "London",
                            postal_code = 14780,
                            country = "United Kingdom"
                        }), IdentityServerConstants.ClaimValueTypes.Json),
                    new Claim(JwtClaimTypes.PhoneNumber, "+447911123456"),
                    new Claim(JwtClaimTypes.PhoneNumberVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.Role, "user")
                }
            }
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResources.Address(),
            new IdentityResources.Phone(),
            new IdentityResource("role", new []{ JwtClaimTypes.Role })
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
         {
             new ApiResource("gateway", "Gateway API")
             {
                Scopes = new[]
                {
                    "identity-service",
                    "movie-service",
                    "payment-service",
                    "review-service",
                    "test-service"
                }
             },
             new ApiResource("identity-service", "Identity Service API")
             {
                Scopes = new[] { "identity-service" }
             },
             new ApiResource("movie-service", "Movie Service API")
             {
                Scopes = new[] { "movie-service" }
             },
             new ApiResource("payment-service", "Payment Service API")
             {
                Scopes = new[] { "payment-service" }
             },
             new ApiResource("review-service", "Review Service API")
             {
                Scopes = new[] { "review-service" }
             },
             new ApiResource("test-service", "Test Service API")
             {
                Scopes = new[] { "test-service" }
             }
         };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("identity-service", "Identity Service API"),
            new ApiScope("movie-service", "Movie Service API"),
            new ApiScope("payment-service", "Payment Service API"),
            new ApiScope("review-service", "Review Service API"),
            new ApiScope("test-service", "Test Service API")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "console",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "api" },
            },
            new Client
            {
                ClientId = "api_client",
                ClientName = "API client",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                RedirectUris =
                {
                    "https://localhost:9000/oauth2-redirect.html",
                    "https://localhost:9101/oauth2-redirect.html",
                    "https://localhost:9102/oauth2-redirect.html",
                    "https://localhost:9103/oauth2-redirect.html",
                    "https://localhost:9104/oauth2-redirect.html"
                },
                AllowedCorsOrigins =
                {
                    "https://localhost:9000",
                    "https://localhost:9101",
                    "https://localhost:9102",
                    "https://localhost:9103",
                    "https://localhost:9104"
                },
                AllowedScopes =
                {
                    "identity-service",
                    "movie-service",
                    "payment-service",
                    "review-service",
                    "test-service"
                }
            }
        };
}