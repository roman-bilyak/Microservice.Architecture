using Microservice.Database;

namespace Microservice.IdentityService.Identity;

internal sealed class FindRoleByIdSpecification : Specification<Role>
{
    public FindRoleByIdSpecification(Guid roleId)
       : base(x => x.Id == roleId)
    {
    }
}