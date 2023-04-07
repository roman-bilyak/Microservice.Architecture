using Microservice.Core;
using Microsoft.AspNetCore.Identity;

namespace Microservice.IdentityService.Identity;

public static class IdentityResultExtensions
{
    public static void CheckErrors(this IdentityResult identityResult)
    {
        if (identityResult.Succeeded)
        {
            return;
        }

        IDictionary<string, string[]> errors = identityResult.Errors
            .GroupBy(x => x.Code)
            .ToDictionary(x => x.Key, x => x.Select(y => y.Description).ToArray());

        throw new DataValidationException(errors);
    }
}