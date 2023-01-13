using Microservice.AspNetCore.Authentication;
using Microservice.AspNetCore.Authorization;
using Microservice.AspNetCore.Swagger;
using Microservice.Core.Modularity;

namespace Microservice.Api
{
    [DependsOn<AuthenticationModule>]
    [DependsOn<AuthorizationModule>]
    [DependsOn<SwaggerModule>]
    public sealed class ApiModule : StartupModule
    {
    }
}