using System.ComponentModel.DataAnnotations;

namespace Microservice.MovieService.MovieManagement;

public class UpdateMovieDto
{
    [Required]
    public string Title { get; set; }
}