using Microservice.AspNetCore;
using Microservice.AspNetCore.Swagger;
using Microservice.Core.Modularity;

namespace Microservice.Api
{
    [DependsOn(typeof(SwaggerModule))]
    [DependsOn(typeof(AspNetCoreModule))]
    public sealed class ApiModule : StartupModule
    {

    }
}