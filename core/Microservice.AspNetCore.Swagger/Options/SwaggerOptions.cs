using System.ComponentModel.DataAnnotations;

namespace Microservice.AspNetCore.Swagger;

public class SwaggerOptions
{
    public const string Swagger = "Swagger";

    [Required]
    public string? Title { get; set; }

    [Required]
    public string? Version { get; set; }

    public SwaggerSecurity? Security { get; set; }

    public string[]? IgnorePaths { get; set; }
}