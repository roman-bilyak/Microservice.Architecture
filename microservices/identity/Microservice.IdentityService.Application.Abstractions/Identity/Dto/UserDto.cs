namespace Microservice.IdentityService.Identity;

public record UserDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;
}
