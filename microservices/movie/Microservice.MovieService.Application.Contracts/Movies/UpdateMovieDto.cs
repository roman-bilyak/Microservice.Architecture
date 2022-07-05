using System.ComponentModel.DataAnnotations;

namespace Microservice.MovieService.Movies;

public class UpdateMovieDto
{
    [Required]
    public string Title { get; set; }
}