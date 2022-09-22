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
}