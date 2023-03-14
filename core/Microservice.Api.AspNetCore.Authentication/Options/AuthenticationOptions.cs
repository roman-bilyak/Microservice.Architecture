namespace Microservice.AspNetCore.Authentication;

public class AuthenticationOptions
{
    public string? Scheme { get; set; }

    public JwtBearerOptions? JwtBearer { get; set; }
}