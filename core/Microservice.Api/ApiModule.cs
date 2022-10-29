using Microservice.AspNetCore.Authentication;
using Microservice.AspNetCore.Authorization;
using Microservice.AspNetCore.Swagger;
using Microservice.Core.Modularity;

namespace Microservice.Api
{
    [DependsOn(typeof(AuthenticationModule))]
    [DependsOn(typeof(AuthorizationModule))]
    [DependsOn(typeof(SwaggerModule))]
    public sealed class ApiModule : StartupModule
    {
    }
}