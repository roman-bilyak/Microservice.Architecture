using Microservice.AspNetCore;
using Microservice.Core;
using Microservice.MovieService;
using Microservice.Swagger;

var builder = WebApplication.CreateBuilder(args);
builder.AddApplication()
    .AddModule<AspNetCoreModule>()
    .AddModule<SwaggerModule>()
    .AddModule<MovieServiceDomainModule>()
    .AddModule<MovieServiceInfrastructureModule>()
    .AddModule<MovieServiceApplicationContractsModule>()
    .AddModule<MovieServiceApplicationModule>()
    .AddModule<MovieServiceWebModule>()
    .ConfigureServices();

var app = builder.Build();
app.UseApplication();

app.Run();