using Microservice.Core.Modularity;
using Microservice.ReviewService.Application.Reviews;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.ReviewService.Application;

public sealed class ReviewServiceApplicationModule : Module
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddTransient<IMovieApplicationService, MovieApplicationService>();
        services.AddTransient<IReviewApplicationService, ReviewApplicationService>();
        services.AddTransient<IUserApplicationService, UserApplicationService>();
    }
}
