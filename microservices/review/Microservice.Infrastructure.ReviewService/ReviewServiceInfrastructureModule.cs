using Microservice.Core.Modularity;
using Microservice.Database;
using Microservice.ReviewService.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.ReviewService;

[DependsOn<ReviewServiceDomainModule>]
public sealed class ReviewServiceInfrastructureModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddDbContext<ReviewServiceDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(nameof(ReviewServiceDbContext)));
        });

        services.AddTransient<IRepository<Review>, BaseRepository<ReviewServiceDbContext, Review>>();
        services.AddTransient<IRepository<Review, Guid>, BaseRepository<ReviewServiceDbContext, Review, Guid>>();
        services.AddTransient<IReadRepository<Review>, BaseRepository<ReviewServiceDbContext, Review>>();
        services.AddTransient<IReadRepository<Review, Guid>, BaseRepository<ReviewServiceDbContext, Review, Guid>>();
    }
}