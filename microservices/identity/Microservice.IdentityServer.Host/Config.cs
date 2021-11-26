using IdentityServer4.Models;

namespace Microservice.IdentityServer.Host;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {

        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {

        };
}