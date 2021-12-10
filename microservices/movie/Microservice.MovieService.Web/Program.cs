using Microservice.AspNetCore;
using Microservice.Core;
using Microservice.MovieService.Application;
using Microservice.MovieService.Domain;
using Microservice.MovieService.Infrastructure;
using Microservice.MovieService.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication()
    .AddModule<AspNetCoreModule>()
    .AddModule<MovieServiceDomainModule>()
    .AddModule<MovieServiceInfrastructureModule>()
    .AddModule<MovieServiceApplicationModule>()
    .AddModule<MovieServiceWebModule>()
    .Configure();

var app = builder.Build();
app.UseApplication();

app.Run();