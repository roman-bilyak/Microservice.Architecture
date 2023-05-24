using FluentValidation;
using Microservice.Application;
using Microservice.Core.Modularity;
using Microservice.ReviewService.Reviews;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.ReviewService;

[DependsOn<ReviewServiceDomainModule>]
public sealed class ReviewServiceApplicationModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddTransient<IValidator<CreateReviewDto>, CreateReviewDtoValidator>();
        services.AddTransient<IValidator<UpdateReviewDto>, UpdateReviewDtoValidator>();

        services.AddTransient<IMovieApplicationService, MovieApplicationService>();
        services.AddTransient<IUserApplicationService, UserApplicationService>();

        services.AddCQRS(typeof(ReviewServiceApplicationModule).Assembly);
    }
}