using Microservice.Database;

namespace Microservice.MovieService.Movies;

internal sealed class FindMovieByIdSpecification : Specification<Movie>
{
    public FindMovieByIdSpecification(Guid movieId)
        : base(x => x.Id == movieId)
    {

    }
}