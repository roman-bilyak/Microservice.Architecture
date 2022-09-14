using Microservice.Application.Services;
using Microservice.AspNetCore;
using Microservice.Core.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.TestService;

public class TestServiceApplicationContractsModule : BaseModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(TestServiceApplicationContractsModule).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });
    }
}