using Microservice.Core.Modularity;
using Microservice.TestService.Tests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.TestService;

[DependsOn<TestServiceDomainModule>]
public sealed class TestServiceApplicationModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddTransient<ITestsApplicationService, TestsApplicationService>();
    }
}