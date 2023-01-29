namespace Microservice.AspNetCore.Authentication;

public class JwtBearerOptions
{
    public string? Authority { get; set; }

    public string? ValidIssuer { get; set; }

    public string? Audience { get; set; }

    public bool? RequireHttpsMetadata { get; set; }
}