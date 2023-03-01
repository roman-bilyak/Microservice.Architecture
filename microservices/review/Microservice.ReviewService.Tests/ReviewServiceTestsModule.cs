using Microservice.Core.Modularity;

namespace Microservice.ReviewService;

[DependsOn<ReviewServiceApplicationModule>]
[DependsOn<ReviewServiceInfrastructureModule>]
internal sealed class ReviewServiceTestsModule : StartupModule
{
}