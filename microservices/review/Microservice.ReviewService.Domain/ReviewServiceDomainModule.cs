using Microservice.Core.Modularity;
using Microservice.ReviewService.Reviews;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.ReviewService;

public sealed class ReviewServiceDomainModule : BaseModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddTransient<IReviewManager, ReviewManager>();
    }
}