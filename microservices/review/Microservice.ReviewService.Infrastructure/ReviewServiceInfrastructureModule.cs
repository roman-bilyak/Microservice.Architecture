using Microservice.Core.Database;
using Microservice.Core.Modularity;
using Microservice.Infrastructure.Database.EntityFrameworkCore;
using Microservice.ReviewService.Database;
using Microservice.ReviewService.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.ReviewService;

public sealed class ReviewServiceInfrastructureModule : BaseModule
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddDbContext<ReviewServiceDbContext>(options =>
        {
            options.UseInMemoryDatabase(nameof(ReviewServiceDbContext));
        });

        services.AddTransient<IRepository<Review>, BaseRepository<ReviewServiceDbContext, Review>>();
        services.AddTransient<IReadRepository<Review>, BaseRepository<ReviewServiceDbContext, Review>>();
        services.AddTransient<IRepository<Review, Guid>, BaseRepository<ReviewServiceDbContext, Review, Guid>>();
        services.AddTransient<IReadRepository<Review, Guid>, BaseRepository<ReviewServiceDbContext, Review, Guid>>();
    }
}