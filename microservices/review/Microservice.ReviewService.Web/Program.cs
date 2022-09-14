using Microservice.AspNetCore;
using Microservice.AspNetCore.Swagger;
using Microservice.Core;
using Microservice.ReviewService;

var builder = WebApplication.CreateBuilder(args);
builder.AddApplication()
    .AddModule<AspNetCoreModule>()
    .AddModule<SwaggerModule>()
    .AddModule<ReviewServiceDomainModule>()
    .AddModule<ReviewServiceInfrastructureModule>()
    .AddModule<ReviewServiceApplicationContractsModule>()
    .AddModule<ReviewServiceApplicationModule>()
    .AddModule<ReviewServiceWebModule>()
    .ConfigureServices();

var app = builder.Build();
app.UseApplication();

app.Run();