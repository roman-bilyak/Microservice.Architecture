namespace Microservice.AspNetCore.Swagger;

public class SwaggerOptions
{
    public const string Swagger = "Swagger";

    public string? Title { get; set; }

    public string? Version { get; set; }

    public SwaggerSecurity? Security { get; set; }

    public string[]? IgnorePaths { get; set; }
}