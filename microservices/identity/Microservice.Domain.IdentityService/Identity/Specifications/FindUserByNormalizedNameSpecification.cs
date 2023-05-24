using Microservice.Database;

namespace Microservice.IdentityService.Identity;

internal sealed class FindUserByNormalizedNameSpecification : Specification<User>
{
    public FindUserByNormalizedNameSpecification(string normalizedName)
        : base(x => x.NormalizedName == normalizedName)
    {
    }
}