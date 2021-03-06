using Microservice.Application.CQRS;
using Microservice.Core.Modularity;
using Microservice.ReviewService.Reviews;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.ReviewService;

public sealed class ReviewServiceApplicationModule : BaseModule
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddCQRS(typeof(ReviewServiceApplicationModule).Assembly);

        services.AddTransient<IMovieApplicationService, MovieApplicationService>();
        services.AddTransient<IReviewApplicationService, ReviewApplicationService>();
        services.AddTransient<IUserApplicationService, UserApplicationService>();
    }
}