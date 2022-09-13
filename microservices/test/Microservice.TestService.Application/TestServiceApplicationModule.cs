using Microservice.Core.Modularity;
using Microservice.TestService.Tests;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.TestService;

public class TestServiceApplicationModule : BaseModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddTransient<ITestApplicationService, TestApplicationService>();
    }
}