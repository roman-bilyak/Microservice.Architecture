using Microservice.Core.Modularity;
using Microservice.TestService.Tests;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.TestService;

[DependsOn<TestServiceDomainModule>]
public sealed class TestServiceApplicationModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddTransient<ITestApplicationService, TestApplicationService>();
    }
}