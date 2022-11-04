using MassTransit;
using Microservice.CQRS;

namespace Microservice.MovieService.Movies;

public class GetMovieByIdQuery : ItemQuery<Guid>
{
    public GetMovieByIdQuery(Guid id) : base(id)
    {
    }

    public class GetMovieByIdQueryHandler : IQueryHandler<GetMovieByIdQuery>
    {
        private readonly IMovieManager _movieManager;

        public GetMovieByIdQueryHandler(IMovieManager movieManager)
        {
            ArgumentNullException.ThrowIfNull(movieManager, nameof(movieManager));

            _movieManager = movieManager;
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