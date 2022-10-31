using System.ComponentModel.DataAnnotations;

namespace Microservice.MovieService.Movies;

public class CreateMovieDto
{
    [Required]
    public string Title { get; set; }
}