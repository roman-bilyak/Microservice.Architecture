using MassTransit;
using Microservice.CQRS;

namespace Microservice.MovieService.Movies;

public class UpdateMovieCommand : UpdateCommand<Guid, UpdateMovieDto>
{
    public UpdateMovieCommand(Guid id, UpdateMovieDto model) : base(id, model)
    {
    }

    public class UpdateMovieCommandHandler : ICommandHandler<UpdateMovieCommand>
    {
        private readonly IMovieManager _movieManager;

        public UpdateMovieCommandHandler(IMovieManager movieManager)
        {
            ArgumentNullException.ThrowIfNull(movieManager, nameof(movieManager));

            _movieManager = movieManager;
        }

        public async Task Consume(ConsumeContext<UpdateMovieCommand> context)
        {
            Movie? movie = await _movieManager.FindByIdAsync(context.Message.Id, context.CancellationToken);
            if (movie is null)
            {
                throw new Exception($"Movie (id = '{context.Message.Id}') not found");
            }

            movie.SetTitle(context.Message.Model.Title);

            movie = await _movieManager.UpdateAsync(movie, context.CancellationToken);
            await _movieManager.SaveChangesAsync(context.CancellationToken);

            await context.RespondAsync(new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title
            });
        }
    }
}