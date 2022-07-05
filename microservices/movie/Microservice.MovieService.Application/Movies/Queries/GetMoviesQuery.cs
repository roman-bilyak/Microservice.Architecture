using Microservice.Application.CQRS.Queries;

namespace Microservice.MovieService.Movies.Queries
{
    internal class GetMoviesQuery : ListQuery<MovieListDto>
    {
        internal class GetMoviesQueryHandler : IQueryHandler<GetMoviesQuery, MovieListDto>
        {
            private readonly IMovieManager _movieManager;

            public GetMoviesQueryHandler(IMovieManager movieManager)
            {
                _movieManager = movieManager;
            }

            public async Task<MovieListDto> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
            {
                MovieListDto result = new MovieListDto
                {
                    TotalCount = await _movieManager.CountAsync(cancellationToken)
                };

                List<Movie> movies = await _movieManager.ListAsync(request.PageIndex, request.PageSize, cancellationToken);
                foreach (Movie movie in movies)
                {
                    result.Items.Add(new MovieDto
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                    });
                }

                return result;
            }
        }
    }
}