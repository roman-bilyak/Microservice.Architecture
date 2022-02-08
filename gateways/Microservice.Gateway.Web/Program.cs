using Microservice.Core;
using Microservice.Gateway;
using Microservice.Infrastructure.AspNetCore;
using Microservice.MovieService;
using Microservice.ReviewService;
using Microservice.TestService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication()
    .AddModule<AspNetCoreModule>()
    .AddModule<MovieServiceApplicationContractsModule>()
    .AddModule<ReviewServiceApplicationContractsModule>()
    .AddModule<TestServiceApplicationContractsModule>()
    .AddModule<GatewayWebModule>()
    .Configure();

var app = builder.Build();
app.UseApplication();

app.Run();
