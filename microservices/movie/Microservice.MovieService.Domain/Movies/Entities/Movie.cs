using Microservice.Database;

namespace Microservice.MovieService.Movies;

public class Movie : Entity<Guid>, IAggregateRoot
{
    public string Title { get; protected internal set; }

    protected Movie()
    {

    }

    public Movie(Guid id, string title)
        : base(id)
    {
        SetTitle(title);
    }

    public void SetTitle(string title)
    {
        ArgumentNullException.ThrowIfNull(title, nameof(title));

        Title = title;
    }
}