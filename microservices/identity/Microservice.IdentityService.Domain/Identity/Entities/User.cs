using Microservice.Database;
using System.Collections.ObjectModel;

namespace Microservice.IdentityService.Identity;

public class User : Entity<Guid>, IAggregateRoot
{
    public string UserName { get; protected internal set; }

    public string NormalizedUserName { get; protected internal set; }

    public string FirstName { get; protected set; }

    public string LastName { get; protected set; }

    public string Email { get; protected set; }

    public string NormalizedEmail { get; protected set; }

    public bool IsEmailConfirmed { get; protected set; }

    public string PasswordHash { get; protected internal set; }

    public virtual ICollection<UserRole> Roles { get; protected set; }

    protected User()
    {
    }

    public User
    (
        Guid id,
        string userName,
        string firstName,
        string lastName,
        string email
    ) : base(id)
    {
        ArgumentNullException.ThrowIfNull(userName, nameof(userName));
        ArgumentNullException.ThrowIfNull(firstName, nameof(firstName));
        ArgumentNullException.ThrowIfNull(lastName, nameof(lastName));
        ArgumentNullException.ThrowIfNull(email, nameof(email));

        UserName = userName;
        NormalizedUserName = userName.ToUpperInvariant();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        NormalizedEmail = email.ToUpperInvariant();

        Roles = new Collection<UserRole>();
    }
}