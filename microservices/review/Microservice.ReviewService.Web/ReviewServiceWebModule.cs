using Microservice.Api;
using Microservice.Core.Modularity;

namespace Microservice.ReviewService;

[DependsOn(typeof(ReviewServiceApplicationModule))]
[DependsOn(typeof(ReviewServiceInfrastructureModule))]
[DependsOn(typeof(ApiModule))]
public sealed class ReviewServiceWebModule : StartupModule
{
}