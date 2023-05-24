namespace Microservice.IdentityService.Identity;

public record UpdateRoleDto
{
    public string Name { get; init; } = string.Empty;
}