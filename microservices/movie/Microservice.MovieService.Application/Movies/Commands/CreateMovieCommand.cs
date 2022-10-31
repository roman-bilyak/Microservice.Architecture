using MassTransit;
using Microservice.CQRS;

namespace Microservice.MovieService.Movies;

public class CreateMovieCommand : CreateCommand<CreateMovieDto>
{
    public CreateMovieCommand(CreateMovieDto model) : base(model)
    {
    }

    public class CreateMovieCommandHandler : ICommandHandler<CreateMovieCommand>
    {
        private readonly IMovieManager _movieManager;

        public CreateMovieCommandHandler(IMovieManager movieManager)
        {
            ArgumentNullException.ThrowIfNull(movieManager, nameof(movieManager));

            _movieManager = movieManager;
        }

        public async Task Consume(ConsumeContext<CreateMovieCommand> context)
        {
            CreateMovieDto movieDto = context.Message.Model;
            Movie movie = new Movie(Guid.NewGuid(), movieDto.Title);

            movie = await _movieManager.AddAsync(movie, context.CancellationToken);
            await _movieManager.SaveChangesAsync(context.CancellationToken);

            await context.RespondAsync(new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title
            });
        }
    }
}