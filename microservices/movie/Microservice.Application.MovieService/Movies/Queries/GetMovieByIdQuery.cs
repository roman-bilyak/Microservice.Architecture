using Microservice.Application;
using Microservice.Core;

namespace Microservice.MovieService.Movies;

public class GetMovieByIdQuery : ItemQuery<Guid, MovieDto>
{
    public GetMovieByIdQuery(Guid id) : base(id)
    {
    }

    public class GetMovieByIdQueryHandler : QueryHandler<GetMovieByIdQuery, MovieDto>
    {
        private readonly IMovieManager _movieManager;

        public GetMovieByIdQueryHandler(IMovieManager movieManager)
        {
            ArgumentNullException.ThrowIfNull(movieManager, nameof(movieManager));

            _movieManager = movieManager;
        }

        protected override async Task<MovieDto> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            Movie? movie = await _movieManager.FindByIdAsync(request.Id, cancellationToken);
            if (movie is null)
            {
                throw new EntityNotFoundException(typeof(Movie), request.Id);
            }

            return new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title
            };
        }
    }
}