using Microservice.CQRS;

namespace Microservice.MovieService.Movies;

public class GetMoviesQuery : ListQuery<MovieListDto>
{
    public GetMoviesQuery(int pageIndex, int pageSize) : base(pageIndex, pageSize)
    {
    }

    public class GetMoviesQueryHandler : QueryHandler<GetMoviesQuery, MovieListDto>
    {
        private readonly IMovieManager _movieManager;

        public GetMoviesQueryHandler(IMovieManager movieManager)
        {
            ArgumentNullException.ThrowIfNull(movieManager, nameof(movieManager));

            _movieManager = movieManager;
        }

        protected override async Task<MovieListDto> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            MovieListDto result = new()
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