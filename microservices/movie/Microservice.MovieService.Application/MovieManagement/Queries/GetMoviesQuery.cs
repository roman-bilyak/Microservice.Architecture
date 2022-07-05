using Microservice.Core.CQRS.Queries;

namespace Microservice.MovieService.MovieManagement.Queries
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
                List<Movie> movies = await _movieManager.ListAsync(request.PageIndex, request.PageSize, cancellationToken);
                int totalCount = await _movieManager.CountAsync(cancellationToken);

                List<MovieDto> items = new List<MovieDto>();
                foreach (Movie movie in movies)
                {
                    items.Add(new MovieDto
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                    });
                }

                return new MovieListDto
                {
                    Items = items,
                    TotalCount = totalCount,
                };
            }
        }
    }
}