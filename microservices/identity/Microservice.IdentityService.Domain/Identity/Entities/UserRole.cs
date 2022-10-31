using Microservice.Database;

namespace Microservice.IdentityService.Identity;

public class UserRole : Entity<Guid>
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }
}