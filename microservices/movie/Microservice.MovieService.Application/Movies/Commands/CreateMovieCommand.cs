using Microservice.CQRS;

namespace Microservice.MovieService.Movies;

public class CreateMovieCommand : CreateCommand<CreateMovieDto, MovieDto>
{
    public CreateMovieCommand(CreateMovieDto model) : base(model)
    {
    }

    public class CreateMovieCommandHandler : CommandHandler<CreateMovieCommand, MovieDto>
    {
        private readonly IMovieManager _movieManager;

        public CreateMovieCommandHandler(IMovieManager movieManager)
        {
            ArgumentNullException.ThrowIfNull(movieManager, nameof(movieManager));

            _movieManager = movieManager;
        }

        protected override async Task<MovieDto> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            CreateMovieDto movieDto = request.Model;
            Movie movie = new(Guid.NewGuid(), movieDto.Title);

            movie = await _movieManager.AddAsync(movie, cancellationToken);
            await _movieManager.SaveChangesAsync(cancellationToken);

            return new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title
            };
        }
    }
}