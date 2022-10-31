using Microservice.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Microservice.IdentityService.Identity;

public class RoleManager : RoleManager<Role>, IDomainService
{
    public RoleManager
    (
        IRoleStore<Role> store,
        IEnumerable<IRoleValidator<Role>> roleValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        ILogger<RoleManager<Role>> logger
    ) : base(store, roleValidators, keyNormalizer, errors, logger)
    {
    }
}