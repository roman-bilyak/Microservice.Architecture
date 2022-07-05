﻿using Microservice.Core.CQRS;
using Microservice.Core.Modularity;
using Microservice.MovieService.MovieManagement;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.MovieService;

public sealed class MovieServiceApplicationModule : BaseModule
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddCQRS(typeof(MovieServiceApplicationModule).Assembly);
        services.AddTransient<IMovieApplicationService, MovieApplicationService>();
    }
}