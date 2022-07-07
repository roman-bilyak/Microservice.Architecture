using MassTransit;
using Microservice.Application.CQRS.Commands;

namespace Microservice.MovieService.Movies.Commands
{
    public class DeleteMovieCommand : DeleteCommand<Guid>
    {
        public class DeleteMovieCommandHandler : ICommandHandler<DeleteMovieCommand>
        {
            private readonly IMovieManager _movieManager;

            public DeleteMovieCommandHandler(IMovieManager movieManager)
            {
                _movieManager = movieManager ?? throw new ArgumentNullException(nameof(movieManager));
            }

            public async Task Consume(ConsumeContext<DeleteMovieCommand> context)
            {
                Movie entity = await _movieManager.GetByIdAsync(context.Message.Id, context.CancellationToken);
                if (entity == null)
                {
                    throw new Exception($"Movie (id = '{context.Message.Id}') not found");
                }

                await _movieManager.DeleteAsync(entity, context.CancellationToken);
                await _movieManager.SaveChangesAsync(context.CancellationToken);
            }
        }
    }
}