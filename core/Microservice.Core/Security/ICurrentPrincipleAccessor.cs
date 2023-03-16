using System.Security.Claims;

namespace Microservice.Core;

public interface ICurrentPrincipleAccessor
{
    ClaimsPrincipal? Principal { get; }
}