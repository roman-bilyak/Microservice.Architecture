using System.Security.Claims;

namespace Microservice.Core;

public class CurrentPrincipleAccessor : ICurrentPrincipleAccessor
{
    public virtual ClaimsPrincipal? Principal => Thread.CurrentPrincipal as ClaimsPrincipal;
}