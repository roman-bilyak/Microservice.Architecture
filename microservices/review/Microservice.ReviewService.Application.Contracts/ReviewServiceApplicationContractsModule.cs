using Microservice.Core.Modularity;
using Microservice.Core.Services;
using Microservice.Infrastructure.AspNetCore.Conventions;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.ReviewService;

public sealed class ReviewServiceApplicationContractsModule : BaseModule
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(ReviewServiceApplicationContractsModule).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });
    }
}
