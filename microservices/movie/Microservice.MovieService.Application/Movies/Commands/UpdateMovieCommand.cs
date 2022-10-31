using MassTransit;
using Microservice.CQRS;

namespace Microservice.MovieService.Movies;

public class UpdateMovieCommand : UpdateCommand<Guid, UpdateMovieDto>
{
    public class UpdateMovieCommandHandler : ICommandHandler<UpdateMovieCommand>
    {
        private readonly IMovieManager _movieManager;

        public UpdateMovieCommandHandler(IMovieManager movieManager)
        {
            _movieManager = movieManager ?? throw new ArgumentNullException(nameof(movieManager));
        }

        public async Task Consume(ConsumeContext<UpdateMovieCommand> context)
        {
            Movie entity = await _movieManager.GetByIdAsync(context.Message.Id, context.CancellationToken);
            if (entity == null)
            {
                throw new Exception($"Movie (id = '{context.Message.Id}') not found");
            }

            entity.Title = context.Message.Model.Title;

            entity = await _movieManager.UpdateAsync(entity, context.CancellationToken);
            await _movieManager.SaveChangesAsync(context.CancellationToken);

            await context.RespondAsync(new MovieDto
            {
                Id = entity.Id,
                Title = entity.Title
            });
        }
    }
}