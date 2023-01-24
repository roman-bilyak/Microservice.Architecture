using System.ComponentModel.DataAnnotations;

namespace Microservice.IdentityService.Identity;

public class CreateRoleDto
{
    [Required]
    public string? Name { get; set; }
}