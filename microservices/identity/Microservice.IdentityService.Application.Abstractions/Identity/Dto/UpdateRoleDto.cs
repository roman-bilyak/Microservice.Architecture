using System.ComponentModel.DataAnnotations;

namespace Microservice.IdentityService.Identity;

public class UpdateRoleDto
{
    [Required]
    public string Name { get; set; }
}
