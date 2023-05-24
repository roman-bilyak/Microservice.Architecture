using Microservice.Database;

namespace Microservice.IdentityService.Identity;

internal sealed class FindUserByNormalizedEmailSpecification : Specification<User>
{
    public FindUserByNormalizedEmailSpecification(string normalizedEmail)
        : base(x => x.NormalizedEmail == normalizedEmail)
    {
    }
}