namespace Microservice.IdentityService.Identity;

public class UpdateUserPasswordDto
{
    public string OldPassword { get; set; }

    public string Password { get; set; }
}