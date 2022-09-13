using Microservice.Application.Services;
using Microservice.Core.Modularity;
using Microservice.Infrastructure.AspNetCore.Conventions;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.ReviewService;

public sealed class ReviewServiceApplicationContractsModule : BaseModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(ReviewServiceApplicationContractsModule).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });
    }
}
