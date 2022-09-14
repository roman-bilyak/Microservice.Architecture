using System.ComponentModel.DataAnnotations;

namespace Microservice.AspNetCore.Swagger;

public class SwaggerSecurityFlow
{
    [Required]
    public GrantTypes? GrantType { get; set; }

    public string? AuthorityUrl { get; set; }

    public string? AuthorizationUrl { get; set; }

    public string? TokenUrl { get; set; }

    public string? RefreshUrl { get; set; }

    public string? ClientId { get; set; }

    public bool? UsePkce { get; set; }

    public Dictionary<string, string>? Scopes { get; set; }
}