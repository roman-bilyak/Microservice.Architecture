using Microservice.Core.Modularity;
using Microservice.Infrastructure.Database;
using Microservice.Infrastructure.Database.EntityFrameworkCore;
using Microservice.ReviewService.Domain.Reviews;
using Microservice.ReviewService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.ReviewService.Infrastructure;

public sealed class ReviewServiceInfrastructureModule : Module
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