using FluentValidation;

namespace Microservice.MovieService.Movies;

internal class UpdateMovieDtoValidator : AbstractValidator<UpdateMovieDto>
{
    public UpdateMovieDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(Movie.MaxTitleLength);
    }
}