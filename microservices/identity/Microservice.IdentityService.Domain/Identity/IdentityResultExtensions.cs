using Microservice.Core;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Microservice.IdentityService.Identity;

public static class IdentityResultExtensions
{
    public static void CheckErrors(this IdentityResult identityResult)
    {
        if (identityResult.Succeeded)
        {
            return;
        }

        ValidationResult[] errors = identityResult.Errors.Select(x => new ValidationResult(x.Description, new[] { x.Code })).ToArray();
        throw new DataValidationException(errors);
    }
}