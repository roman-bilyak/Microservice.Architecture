using System.ComponentModel.DataAnnotations;

namespace Microservice.IdentityService.Identity;

public record UpdateRoleDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; init; } = string.Empty;
}