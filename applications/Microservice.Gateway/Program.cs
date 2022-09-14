using Microservice.Core;
using Microservice.Gateway;
using Microservice.AspNetCore;
using Microservice.AspNetCore.Swagger;
using Microservice.MovieService;
using Microservice.ReviewService;
using Microservice.TestService;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplication()
    .AddModule<AspNetCoreModule>()
    .AddModule<SwaggerModule>()
    .AddModule<MovieServiceApplicationContractsModule>()
    .AddModule<ReviewServiceApplicationContractsModule>()
    .AddModule<TestServiceApplicationContractsModule>()
    .AddModule<GatewayModule>()
    .ConfigureServices();

var app = builder.Build();
app.UseApplication();

app.Run();
