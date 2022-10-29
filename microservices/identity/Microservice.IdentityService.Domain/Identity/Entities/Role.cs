using Microservice.Database;

namespace Microservice.IdentityService.Identity;

public class Role : Entity<Guid>, IAggregateRoot
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}