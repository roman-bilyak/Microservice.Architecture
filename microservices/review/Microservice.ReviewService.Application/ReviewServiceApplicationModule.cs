using Microservice.CQRS;
using Microservice.Core.Modularity;
using Microservice.ReviewService.Reviews;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Microservice.ReviewService;

[DependsOn<ReviewServiceDomainModule>]
public sealed class ReviewServiceApplicationModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddTransient<IMovieApplicationService, MovieApplicationService>();
        services.AddTransient<IReviewApplicationService, ReviewApplicationService>();
        services.AddTransient<IUserApplicationService, UserApplicationService>();

        services.AddCQRS(typeof(ReviewServiceApplicationModule).Assembly);
    }
}