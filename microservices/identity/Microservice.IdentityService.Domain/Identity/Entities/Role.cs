using Microservice.Database;

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
        ArgumentNullException.ThrowIfNull(name, nameof(name));

        Name = name;
        NormalizedName = name.ToUpperInvariant();
    }
}