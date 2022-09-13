using Microservice.Core;
using Microservice.Gateway;
using Microservice.Infrastructure.AspNetCore;
using Microservice.Infrastructure.AspNetCore.Extensions;
using Microservice.MovieService;
using Microservice.ReviewService;
using Microservice.TestService;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplication()
    .AddModule<AspNetCoreModule>()
    .AddModule<MovieServiceApplicationContractsModule>()
    .AddModule<ReviewServiceApplicationContractsModule>()
    .AddModule<TestServiceApplicationContractsModule>()
    .AddModule<GatewayModule>()
    .ConfigureServices();

var app = builder.Build();
app.UseApplication();

app.Run();
