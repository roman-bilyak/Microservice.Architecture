using Microservice.Application.Services;
using Microservice.Core.Modularity;
using Microservice.Infrastructure.AspNetCore.Conventions;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.IdentityService;

public sealed class IdentityServiceApplicationContractsModule : BaseModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(IdentityServiceApplicationContractsModule).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });
    }
}