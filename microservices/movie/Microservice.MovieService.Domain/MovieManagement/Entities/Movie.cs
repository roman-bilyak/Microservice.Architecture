using Microservice.Infrastructure.Database;

namespace Microservice.MovieService.Domain;

public class Movie : Entity<Guid>, IAggregateRoot
{
    public string Title { get; set; }
}