namespace Microservice.IdentityService.Identity;

public record CreateRoleDto
{
    public string Name { get; init; } = string.Empty;
}