using System.ComponentModel.DataAnnotations;

namespace Microservice.AspNetCore.Authentication;

public class AuthenticationOptions
{
    [Required]
    public string? Scheme { get; set; }

    public IdentityServerOptions? IdentityServer { get; set; }
}