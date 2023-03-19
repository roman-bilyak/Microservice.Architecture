using Microservice.Core;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Microservice.Api.AspNetCore.Security;

internal class HttpContextCurrentPrincipleAccessor : CurrentPrincipleAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextCurrentPrincipleAccessor
    (
        IHttpContextAccessor httpContextAccessor
    )
    {
        ArgumentNullException.ThrowIfNull(httpContextAccessor, nameof(httpContextAccessor));

        _httpContextAccessor = httpContextAccessor;
    }

    public override ClaimsPrincipal? Principal => _httpContextAccessor.HttpContext?.User ?? base.Principal;
}