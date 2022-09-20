using Microservice.Application.CQRS;
using Microservice.Core.Modularity;
using Microservice.ReviewService.Reviews;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.ReviewService;

public sealed class ReviewServiceApplicationModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddCQRS(typeof(ReviewServiceApplicationModule).Assembly);

        services.AddTransient<IMovieApplicationService, MovieApplicationService>();
        services.AddTransient<IReviewApplicationService, ReviewApplicationService>();
        services.AddTransient<IUserApplicationService, UserApplicationService>();
    }
}