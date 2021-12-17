using Microservice.AspNetCore;
using Microservice.Core;
using Microservice.MovieService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication()
    .AddModule<AspNetCoreModule>()
    .AddModule<MovieServiceDomainModule>()
    .AddModule<MovieServiceInfrastructureModule>()
    .AddModule<MovieServiceApplicationContractsModule>()
    .AddModule<MovieServiceApplicationModule>()
    .AddModule<MovieServiceWebModule>()
    .Configure();

var app = builder.Build();
app.UseApplication();

app.Run();