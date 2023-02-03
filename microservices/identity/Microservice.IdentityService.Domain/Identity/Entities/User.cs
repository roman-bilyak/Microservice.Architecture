using Microservice.Database;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Microservice.IdentityService.Identity;

public class User : Entity<Guid>, IAggregateRoot
{
    public const int MaxNameLength = 50;
    public const int MaxFirstNameLength = 200;
    public const int MaxLastNameLength = 200;
    public const int MaxEmailLength = 100;
    public const int MaxPasswordLength = 50;

    public string Name { get; protected set; } = string.Empty;

    public string NormalizedName { get; protected set; } = string.Empty;

    public string FirstName { get; protected set; } = string.Empty;

    public string LastName { get; protected set; } = string.Empty;

    public string Email { get; protected set; } = string.Empty;

    public string NormalizedEmail { get; protected set; } = string.Empty;

    public bool IsEmailConfirmed { get; protected set; }

    public string PasswordHash { get; protected set; } = string.Empty;

    public virtual ICollection<UserRole> Roles { get; protected set; } = new Collection<UserRole>();

    protected User()
    {
    }

    public User
    (
        Guid id,
        string name,
        string firstName,
        string lastName,
        string email
    ) : base(id)
    {
        
        SetName(name);
        SetFirstName(firstName);
        SetLastName(lastName);
        SetEmail(email);
    }

    [MemberNotNull(nameof(Name))]
    public void SetName(string name)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));

        Name = name;
    }

    [MemberNotNull(nameof(NormalizedName))]
    public void SetNormalizedName(string normalizedName)
    {
        ArgumentNullException.ThrowIfNull(normalizedName, nameof(normalizedName));

        NormalizedName = normalizedName;
    }

    [MemberNotNull(nameof(FirstName))]
    public void SetFirstName(string firstName)
    {
        ArgumentNullException.ThrowIfNull(firstName, nameof(firstName));

        FirstName = firstName;
    }

    [MemberNotNull(nameof(LastName))]
    public void SetLastName(string lastName)
    {
        ArgumentNullException.ThrowIfNull(lastName, nameof(lastName));

        LastName = lastName;
    }

    [MemberNotNull(nameof(Email))]
    public void SetEmail(string email)
    {
        ArgumentNullException.ThrowIfNull(email, nameof(email));

        Email = email;
    }

    [MemberNotNull(nameof(NormalizedEmail))]
    public void SetNormalizedEmail(string normalizedEmail)
    {
        ArgumentNullException.ThrowIfNull(normalizedEmail, nameof(normalizedEmail));

        NormalizedEmail = normalizedEmail;
    }

    public void SetEmailConfirmed(bool confirmed)
    {
        IsEmailConfirmed = confirmed;
    }

    [MemberNotNull(nameof(PasswordHash))]
    public void SetPasswordHash(string passwordHash)
    {
        ArgumentNullException.ThrowIfNull(passwordHash, nameof(passwordHash));

        PasswordHash = passwordHash;
    }

    public void AddRole(Guid roleId)
    {
        if (IsInRole(roleId))
        {
            return;
        }

        Roles.Add(new UserRole(Id, roleId));
    }

    public void RemoveRole(Guid roleId)
    {
        if (!IsInRole(roleId))
        {
            return;
        }

        Roles.Where(x => x.RoleId == roleId).ToList()
            .ForEach(x => Roles.Remove(x));
    }

    public bool IsInRole(Guid roleId)
    {
        return Roles.Any(x => x.RoleId == roleId);
    }
}