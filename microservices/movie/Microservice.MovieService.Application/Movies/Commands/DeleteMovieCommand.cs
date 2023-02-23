using Microservice.Core;
using Microservice.CQRS;

namespace Microservice.MovieService.Movies;

public class DeleteMovieCommand : DeleteCommand<Guid>
{
    public DeleteMovieCommand(Guid id) : base(id)
    {
    }

    public class DeleteMovieCommandHandler : CommandHandler<DeleteMovieCommand>
    {
        private readonly IMovieManager _movieManager;

        public DeleteMovieCommandHandler(IMovieManager movieManager)
        {
            ArgumentNullException.ThrowIfNull(movieManager, nameof(movieManager));

            _movieManager = movieManager;
        }

        protected override async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            Movie? movie = await _movieManager.FindByIdAsync(request.Id, cancellationToken);
            if (movie is null)
            {
                throw new EntityNotFoundException(typeof(Movie), request.Id);
            }

            await _movieManager.DeleteAsync(movie, cancellationToken);
            await _movieManager.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}