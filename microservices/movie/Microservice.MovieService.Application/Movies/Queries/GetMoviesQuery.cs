using MassTransit;
using Microservice.CQRS;

namespace Microservice.MovieService.Movies;

public class GetMoviesQuery : ListQuery
{
    public class GetMoviesQueryHandler : IQueryHandler<GetMoviesQuery>
    {
        private readonly IMovieManager _movieManager;

        public GetMoviesQueryHandler(IMovieManager movieManager)
        {
            ArgumentNullException.ThrowIfNull(movieManager, nameof(movieManager));

            _movieManager = movieManager;
        }

        public async Task Consume(ConsumeContext<GetMoviesQuery> context)
        {
            MovieListDto result = new MovieListDto
            {
                TotalCount = await _movieManager.CountAsync(context.CancellationToken)
            };

            List<Movie> movies = await _movieManager.ListAsync(context.Message.PageIndex, context.Message.PageSize, context.CancellationToken);
            foreach (Movie movie in movies)
            {
                result.Items.Add(new MovieDto
                {
                    Id = movie.Id,
                    Title = movie.Title,
                });
            }

            await context.RespondAsync(result);
        }
    }
}