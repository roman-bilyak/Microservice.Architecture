using System.ComponentModel.DataAnnotations;

namespace Microservice.MovieService.MovieManagement;

public class CreateMovieDto
{
    [Required]
    public string Title { get; set; }
}