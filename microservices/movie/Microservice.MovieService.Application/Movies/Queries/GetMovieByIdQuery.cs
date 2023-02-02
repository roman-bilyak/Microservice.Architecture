using MassTransit;
using Microservice.Core;
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
            Movie? movie = await _movieManager.FindByIdAsync(context.Message.Id, context.CancellationToken);
            if (movie is null)
            {
                throw new EntityNotFoundException(typeof(Movie), context.Message.Id);
            }

            await context.RespondAsync(new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title
            });
        }
    }
}