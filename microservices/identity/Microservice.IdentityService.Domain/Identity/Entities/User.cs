using Microservice.Database;
using System.Security.Principal;

namespace Microservice.IdentityService.Identity;

public class User : Entity<Guid>, IAggregateRoot, IIdentity
{
    public string Name { get; set; }

    public string UserName { get; set; }

    public string NormalizedUserName { get; internal set; }

    public string Email { get; set; }

    public string NormalizedEmail { get; internal set; }

    public bool EmailConfirmed { get; set; }

    public string PasswordHash { get; set; }

    public string AuthenticationType { get; set; }

    public bool IsAuthenticated { get; set; }

    public virtual ICollection<UserRole> Roles { get; set; }
}