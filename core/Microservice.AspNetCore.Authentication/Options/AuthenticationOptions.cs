using System.ComponentModel.DataAnnotations;

namespace Microservice.AspNetCore.Authentication;

public class AuthenticationOptions
{
    [Required]
    public string? Scheme { get; set; }

    public JwtBearerOptions? JwtBearer { get; set; }
}