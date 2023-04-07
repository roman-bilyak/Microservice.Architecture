using Microservice.Database;

namespace Microservice.IdentityService.Identity;

internal sealed class FindRoleByNormalizedNameSpecification : Specification<Role>
{
    public FindRoleByNormalizedNameSpecification(string normalizedName)
        : base(x => x.NormalizedName == normalizedName)
    {
    }
}