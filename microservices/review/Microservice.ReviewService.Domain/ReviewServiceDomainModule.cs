using Microservice.Core.Modularity;
using Microservice.ReviewService.Domain.Reviews;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.ReviewService.Domain;

public sealed class ReviewServiceDomainModule : Module
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddTransient<IReviewManager, ReviewManager>();
    }
}