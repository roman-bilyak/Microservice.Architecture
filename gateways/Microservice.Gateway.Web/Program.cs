using Microservice.AspNetCore;
using Microservice.Core;
using Microservice.Gateway;
using Microservice.MovieService;
using Microservice.ReviewService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication()
    .AddModule<AspNetCoreModule>()
    .AddModule<MovieServiceApplicationContractsModule>()
    .AddModule<ReviewServiceApplicationContractsModule>()
    .AddModule<GatewayWebModule>()
    .Configure();

var app = builder.Build();
app.UseApplication();

app.Run();
