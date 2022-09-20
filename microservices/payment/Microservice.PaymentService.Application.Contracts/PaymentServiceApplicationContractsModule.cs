using Microservice.Application.Services;
using Microservice.AspNetCore;
using Microservice.Core.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.PaymentService;

public sealed class PaymentServiceApplicationContractsModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(PaymentServiceApplicationContractsModule).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });
    }
}