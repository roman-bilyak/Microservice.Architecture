using Microservice.Database;

namespace Microservice.IdentityService.Identity;

public class UserRole : Entity
{
    public Guid UserId { get; protected set; }

    public Guid RoleId { get; protected set; }

    protected UserRole()
    {

    }

    protected internal UserRole(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}