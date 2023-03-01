using Microservice.Database;

namespace Microservice.IdentityService.Identity;

public sealed class GetRolesSpecification : Specification<Role>
{
    public GetRolesSpecification()
        : base()
    {
    }

    public GetRolesSpecification(List<Guid> ids)
        : base(x => ids.Contains(x.Id))
    {
    }
}
