using Microsoft.OpenApi.Models;

namespace Microservice.AspNetCore.Swagger;

public class SwaggerSecurity
{
    public SecuritySchemeType? Type { get; set; }

    public string? Name { get; set; }

    public string? Scheme { get; set; }

    public SwaggerSecurityFlow? Flow { get; set; }
}