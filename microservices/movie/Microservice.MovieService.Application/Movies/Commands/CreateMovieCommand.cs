using MassTransit;
using Microservice.CQRS;

namespace Microservice.MovieService.Movies;

public class CreateMovieCommand : CreateCommand<CreateMovieDto>
{
    public class CreateMovieCommandHandler : ICommandHandler<CreateMovieCommand>
    {
        private readonly IMovieManager _movieManager;

        public CreateMovieCommandHandler(IMovieManager movieManager)
        {
            _movieManager = movieManager ?? throw new ArgumentNullException(nameof(movieManager));
        }

        public async Task Consume(ConsumeContext<CreateMovieCommand> context)
        {
            Movie entity = new Movie
            {
                Title = context.Message.Model.Title
            };

            entity = await _movieManager.AddAsync(entity, context.CancellationToken);
            await _movieManager.SaveChangesAsync(context.CancellationToken);

            await context.RespondAsync(new MovieDto
            {
                Id = entity.Id,
                Title = entity.Title
            });
        }
    }
}