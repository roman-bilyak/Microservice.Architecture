using Microservice.Database;

namespace Microservice.IdentityService.Identity;

internal sealed class FindUserByNormalizedUserNameSpecification : Specification<User>
{
    public FindUserByNormalizedUserNameSpecification(string normalizedName)
        : base(x => x.NormalizedUserName == normalizedName)
    {
    }
}