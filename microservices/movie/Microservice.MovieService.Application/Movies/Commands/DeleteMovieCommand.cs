using MassTransit;
using Microservice.Core;
using Microservice.CQRS;

namespace Microservice.MovieService.Movies;

public class DeleteMovieCommand : DeleteCommand<Guid>
{
    public DeleteMovieCommand(Guid id) : base(id)
    {
    }

    public class DeleteMovieCommandHandler : ICommandHandler<DeleteMovieCommand>
    {
        private readonly IMovieManager _movieManager;

        public DeleteMovieCommandHandler(IMovieManager movieManager)
        {
            ArgumentNullException.ThrowIfNull(movieManager, nameof(movieManager));

            _movieManager = movieManager;
        }

        public async Task Consume(ConsumeContext<DeleteMovieCommand> context)
        {
            Movie? movie = await _movieManager.FindByIdAsync(context.Message.Id, context.CancellationToken);
            if (movie is null)
            {
                throw new EntityNotFoundException(typeof(Movie), context.Message.Id);
            }

            await _movieManager.DeleteAsync(movie, context.CancellationToken);
            await _movieManager.SaveChangesAsync(context.CancellationToken);
        }
    }
}