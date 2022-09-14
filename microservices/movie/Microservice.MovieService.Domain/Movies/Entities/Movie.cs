using Microservice.Database;

namespace Microservice.MovieService.Movies;

public class Movie : Entity<Guid>, IAggregateRoot
{
    public string Title { get; set; }
}