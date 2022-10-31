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

        string exceptioMessage = string.Join(",", identityResult.Errors.Select(x => x.Description));
        throw new Exception(exceptioMessage);
    }
}
