using Microservice.CQRS;
using Microservice.Core.Modularity;
using Microservice.ReviewService.Reviews;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.ReviewService;

[DependsOn<ReviewServiceDomainModule>]
public sealed class ReviewServiceApplicationModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddTransient<IMovieApplicationService, MovieApplicationService>();
        services.AddTransient<IUserApplicationService, UserApplicationService>();

        services.AddCQRS(typeof(ReviewServiceApplicationModule).Assembly);
    }
}