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

    public Guid? Id
    {
        get
        {
            string? id = GetClaimValue(ClaimTypes.NameIdentifier);
            if (id is null)
            {
                return null;
            }

            if (!Guid.TryParse(id, out Guid result))
            {
                return null;
            }

            return result;
        }
    }

    public string? Name => GetClaimValue(ClaimTypes.Name);

    public string? FirstName => GetClaimValue(ClaimTypes.GivenName);

    public string? LastName => GetClaimValue(ClaimTypes.Surname);

    public string? Email => GetClaimValue(ClaimTypes.Email);

    public bool IsAuthenticated => Id.HasValue;

    #region helper methods

    private string? GetClaimValue(string claimType)
    {
        Claim? claim = _currentPrincipleAccessor.Principal?.Claims.FirstOrDefault(x => x.Type == claimType);
        return claim?.Value;
    }

    #endregion
}