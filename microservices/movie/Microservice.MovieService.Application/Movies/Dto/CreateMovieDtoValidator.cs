using FluentValidation;

namespace Microservice.MovieService.Movies;

internal class CreateMovieDtoValidator : AbstractValidator<CreateMovieDto>
{
    public CreateMovieDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(Movie.MaxTitleLength);
    }
}