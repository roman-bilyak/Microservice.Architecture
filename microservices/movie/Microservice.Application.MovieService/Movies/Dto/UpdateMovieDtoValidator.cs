using FluentValidation;

namespace Microservice.MovieService.Movies;

internal class UpdateMovieDtoValidator : AbstractValidator<UpdateMovieDto>
{
    public UpdateMovieDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Movie.MaxTitleLength);
    }
}