using Microservice.Database;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Xml.Linq;

namespace Microservice.IdentityService.Identity;

public class User : Entity<Guid>, IAggregateRoot
{
    public string Name { get; protected internal set; }

    public string NormalizedName { get; protected internal set; }

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
        string name,
        string firstName,
        string lastName,
        string email
    ) : base(id)
    {
        
        Update(name, firstName, lastName, email); ;

        Roles = new Collection<UserRole>();
    }

    public void Update(string name, string firstName, string lastName, string email)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        ArgumentNullException.ThrowIfNull(firstName, nameof(firstName));
        ArgumentNullException.ThrowIfNull(lastName, nameof(lastName));
        ArgumentNullException.ThrowIfNull(email, nameof(email));

        Name = name;
        NormalizedName = name.ToUpperInvariant();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        NormalizedEmail = email.ToUpperInvariant();
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