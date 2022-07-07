using MassTransit;
using Microservice.Application.CQRS.Queries;

namespace Microservice.MovieService.Movies.Queries
{
    public class GetMovieByIdQuery : ItemQuery<Guid>
    {
        public class GetMovieByIdQueryHandler : IQueryHandler<GetMovieByIdQuery>
        {
            private readonly IMovieManager _movieManager;

            public GetMovieByIdQueryHandler(IMovieManager movieManager)
            {
                _movieManager = movieManager ?? throw new ArgumentNullException(nameof(movieManager));
            }

            public async Task Consume(ConsumeContext<GetMovieByIdQuery> context)
            {
                Movie movie = await _movieManager.GetByIdAsync(context.Message.Id, context.CancellationToken);
                if (movie == null)
                {
                    throw new Exception($"Movie (id = '{context.Message.Id}') not found");
                }

                await context.RespondAsync(new MovieDto
                {
                    Id = movie.Id,
                    Title = movie.Title
                });
            }
        }
    }
}