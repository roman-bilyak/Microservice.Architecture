using FluentValidation;
using FluentValidation.Results;
using Microservice.Application;
using Microservice.Core;

namespace Microservice.MovieService.Movies;

public class CreateMovieCommand : CreateCommand<CreateMovieDto, MovieDto>
{
    public CreateMovieCommand(CreateMovieDto model) : base(model)
    {
    }

    public class CreateMovieCommandHandler : CommandHandler<CreateMovieCommand, MovieDto>
    {
        private readonly IMovieManager _movieManager;
        private readonly IValidator<CreateMovieDto> _validator;

        public CreateMovieCommandHandler(IMovieManager movieManager, IValidator<CreateMovieDto> validator)
        {
            ArgumentNullException.ThrowIfNull(movieManager, nameof(movieManager));
            ArgumentNullException.ThrowIfNull(validator, nameof(validator));

            _movieManager = movieManager;
            _validator = validator;
        }

        protected override async Task<MovieDto> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            CreateMovieDto model = request.Model;
            ValidationResult validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new DataValidationException(validationResult.ToDictionary());
            }

            Movie movie = new(Guid.NewGuid(), model.Title);

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