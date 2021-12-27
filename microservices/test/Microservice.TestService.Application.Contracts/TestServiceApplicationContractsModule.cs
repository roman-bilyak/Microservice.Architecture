using Microservice.AspNetCore.Conventions;
using Microservice.Core.Modularity;
using Microservice.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.TestService;

public class TestServiceApplicationContractsModule : BaseModule
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(TestServiceApplicationContractsModule).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });
    }
}