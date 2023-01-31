using System.ComponentModel.DataAnnotations;

namespace Microservice.IdentityService.Identity;

public class UpdateUserPasswordDto
{
    [Required]
    [MaxLength(50)]
    public string OldPassword { get; init; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Password { get; init; } = string.Empty;
}