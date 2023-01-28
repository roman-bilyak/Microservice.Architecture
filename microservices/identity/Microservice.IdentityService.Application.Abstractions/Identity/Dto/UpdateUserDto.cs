using System.ComponentModel.DataAnnotations;

namespace Microservice.IdentityService.Identity;

public record UpdateUserDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; init; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string FirstName { get; init; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string LastName { get; init; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; init; } = string.Empty;
}
