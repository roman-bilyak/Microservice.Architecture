using Microservice.Database;

namespace Microservice.IdentityService.Identity;

internal sealed class FindUserByIdSpecification : Specification<User>
{
    public FindUserByIdSpecification(Guid id)
        : base(x => x.Id == id)
    {
        AddInclude(x => x.Roles);
    }
}