namespace Microservice.IdentityService.Identity;

public class ChangeUserPasswordDto
{
    public string OldPassword { get; set; }

    public string Password { get; set; }
}
