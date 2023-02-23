using Microservice.Core;
using Microservice.CQRS;

namespace Microservice.MovieService.Movies;

public class UpdateMovieCommand : UpdateCommand<Guid, UpdateMovieDto, MovieDto>
{
    public UpdateMovieCommand(Guid id, UpdateMovieDto model) : base(id, model)
    {
    }

    public class UpdateMovieCommandHandler : CommandHandler<UpdateMovieCommand, MovieDto>
    {
        private readonly IMovieManager _movieManager;

        public UpdateMovieCommandHandler(IMovieManager movieManager)
        {
            ArgumentNullException.ThrowIfNull(movieManager, nameof(movieManager));

            _movieManager = movieManager;
        }

        protected override async Task<MovieDto> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            Movie? movie = await _movieManager.FindByIdAsync(request.Id, cancellationToken);
            if (movie is null)
            {
                throw new EntityNotFoundException(typeof(Movie), request.Id);
            }

            movie.SetTitle(request.Model.Title);

            movie = await _movieManager.UpdateAsync(movie, cancellationToken);
            await _movieManager.SaveChangesAsync(cancellationToken);

            return new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title
            };
        }
    }
}