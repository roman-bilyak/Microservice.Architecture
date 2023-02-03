namespace Microservice.IdentityService.Identity;

public class UpdateUserPasswordDto
{
    public string OldPassword { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;
}