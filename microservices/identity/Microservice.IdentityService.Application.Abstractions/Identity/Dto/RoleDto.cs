namespace Microservice.IdentityService.Identity;

public record RoleDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;
}
