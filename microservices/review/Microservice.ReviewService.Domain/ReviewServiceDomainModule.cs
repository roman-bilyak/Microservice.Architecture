using Microservice.Core.Modularity;
using Microservice.ReviewService.Reviews;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.ReviewService;

public sealed class ReviewServiceDomainModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddTransient<IReviewManager, ReviewManager>();
    }
}