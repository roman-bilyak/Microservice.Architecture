using System.Security.Authentication;
using System.Security.Claims;

namespace Microservice.Core;

internal class CurrentUser : ICurrentUser
{
    private readonly ICurrentPrincipleAccessor _currentPrincipleAccessor;

    public CurrentUser
    (
        ICurrentPrincipleAccessor currentPrincipleAccessor
    )
    {
        ArgumentNullException.ThrowIfNull(currentPrincipleAccessor, nameof(currentPrincipleAccessor));

        _currentPrincipleAccessor = currentPrincipleAccessor;
    }

    public Guid Id
    {
        get
        {
            string? id = GetClaimValue(ClaimTypes.NameIdentifier);
            if (id is null || !Guid.TryParse(id, out Guid result))
            {
                throw new AuthenticationException();
            }

            return result;
        }
    }

    public string Name => GetClaimValue(ClaimTypes.Name) ?? throw new AuthenticationException();

    public string FirstName => GetClaimValue(ClaimTypes.GivenName) ?? throw new AuthenticationException();

    public string LastName => GetClaimValue(ClaimTypes.Surname) ?? throw new AuthenticationException();

    public string Email => GetClaimValue(ClaimTypes.Email) ?? throw new AuthenticationException();

    public bool IsAuthenticated => GetClaimValue(ClaimTypes.NameIdentifier) is not null;

    #region helper methods

    private string? GetClaimValue(string claimType)
    {
        Claim? claim = _currentPrincipleAccessor.Principal?.Claims.FirstOrDefault(x => x.Type == claimType);
        return claim?.Value;
    }

    #endregion
}