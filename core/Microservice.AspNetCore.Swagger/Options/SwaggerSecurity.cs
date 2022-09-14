using Microsoft.OpenApi.Models;
using System.ComponentModel.DataAnnotations;

namespace Microservice.AspNetCore.Swagger;

public class SwaggerSecurity
{
    [Required]
    public SecuritySchemeType? Type { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Scheme { get; set; }

    [Required]
    public SwaggerSecurityFlow? Flow { get; set; }
}