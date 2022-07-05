using Microservice.Core.CQRS.Queries;

namespace Microservice.MovieService.Movies.Queries
{
    internal class GetMovieByIdQuery : ItemQuery<Guid, MovieDto>
    {
        internal class GetMovieByIdQueryHandler : IQueryHandler<GetMovieByIdQuery, MovieDto>
        {
            private readonly IMovieManager _movieManager;

            public GetMovieByIdQueryHandler(IMovieManager movieManager)
            {
                _movieManager = movieManager;
            }

            public async Task<MovieDto> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
            {
                Movie movie = await _movieManager.GetByIdAsync(request.Id, cancellationToken);
                if (movie == null)
                {
                    throw new Exception($"Movie (id = '{request.Id}') not found");
                }

                return new MovieDto
                {
                    Id = movie.Id,
                    Title = movie.Title
                };
            }
        }
    }
}