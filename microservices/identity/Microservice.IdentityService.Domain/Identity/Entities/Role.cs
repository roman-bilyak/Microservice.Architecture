using Microservice.Database;
using System.Diagnostics.CodeAnalysis;

namespace Microservice.IdentityService.Identity;

public class Role : Entity<Guid>, IAggregateRoot
{
    public string Name { get; protected internal set; }

    public string NormalizedName { get; protected internal set; }

    protected Role()
    {
    }

    public Role
    (
        Guid id,
        string name
    ) : base(id)
    {
        Update(name);
    }

    [MemberNotNull(nameof(Name))]
    [MemberNotNull(nameof(NormalizedName))]
    public void Update(string name)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));

        Name = name;
        NormalizedName = name.ToUpperInvariant();
    }
}