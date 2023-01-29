using System.ComponentModel.DataAnnotations;

namespace Microservice.IdentityService.Identity;

public record ChangeUserPasswordDto
{
    [Required]
    [MaxLength(50)]
    public string OldPassword { get; init; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Password { get; init; } = string.Empty;
}
