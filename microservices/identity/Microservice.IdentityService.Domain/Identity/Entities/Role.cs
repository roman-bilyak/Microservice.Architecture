using Microservice.Database;
using System.Diagnostics.CodeAnalysis;

namespace Microservice.IdentityService.Identity;

public class Role : Entity<Guid>, IAggregateRoot
{
    public string Name { get; protected set; } = string.Empty;

    public string NormalizedName { get; protected set; } = string.Empty;

    protected Role()
    {
    }

    public Role
    (
        Guid id,
        string name
    ) : base(id)
    {
        SetName(name);
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
}