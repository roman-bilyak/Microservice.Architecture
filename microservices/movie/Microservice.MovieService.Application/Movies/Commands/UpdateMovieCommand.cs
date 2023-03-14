using FluentValidation;
using FluentValidation.Results;
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
        private readonly IValidator<UpdateMovieDto> _validator;

        public UpdateMovieCommandHandler(IMovieManager movieManager, IValidator<UpdateMovieDto> validator)
        {
            ArgumentNullException.ThrowIfNull(movieManager, nameof(movieManager));
            ArgumentNullException.ThrowIfNull(validator, nameof(validator));

            _movieManager = movieManager;
            _validator = validator;
        }

        protected override async Task<MovieDto> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            UpdateMovieDto model = request.Model;
            ValidationResult validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new DataValidationException(validationResult.ToDictionary());
            }

            Movie? movie = await _movieManager.FindByIdAsync(request.Id, cancellationToken);
            if (movie is null)
            {
                throw new EntityNotFoundException(typeof(Movie), request.Id);
            }

            movie.SetTitle(model.Title);

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