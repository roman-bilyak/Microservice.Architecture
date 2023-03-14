using FluentValidation;
using Microservice.Application;
using Microservice.Core.Modularity;
using Microservice.MovieService.Movies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.MovieService;

[DependsOn<MovieServiceDomainModule>]
public sealed class MovieServiceApplicationModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddScoped<IValidator<CreateMovieDto>, CreateMovieDtoValidator>();
        services.AddScoped<IValidator<UpdateMovieDto>, UpdateMovieDtoValidator>();

        services.AddTransient<IMovieApplicationService, MovieApplicationService>();

        services.AddCQRS(typeof(MovieServiceApplicationModule).Assembly);
    }
}