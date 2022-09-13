using Microservice.Core;
using Microservice.Infrastructure.AspNetCore;
using Microservice.Infrastructure.AspNetCore.Extensions;
using Microservice.MovieService;

var builder = WebApplication.CreateBuilder(args);
builder.AddApplication()
    .AddModule<AspNetCoreModule>()
    .AddModule<MovieServiceDomainModule>()
    .AddModule<MovieServiceInfrastructureModule>()
    .AddModule<MovieServiceApplicationContractsModule>()
    .AddModule<MovieServiceApplicationModule>()
    .AddModule<MovieServiceWebModule>()
    .ConfigureServices();

var app = builder.Build();
app.UseApplication();

app.Run();