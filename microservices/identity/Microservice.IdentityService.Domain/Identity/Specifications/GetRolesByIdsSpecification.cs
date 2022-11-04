using Microservice.Database;

namespace Microservice.IdentityService.Identity;

public sealed class GetRolesByIdsSpecification : Specification<Role>
{
    public GetRolesByIdsSpecification(List<Guid> ids)
        : base(x => ids.Contains(x.Id))
    {
    }
}
