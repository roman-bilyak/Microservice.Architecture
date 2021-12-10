using Microservice.Infrastructure.Database;

namespace Microservice.MovieService.Domain.MovieManagement;

public class Movie : Entity<Guid>, IAggregateRoot
{
    public string Title { get; set; }
}