using Microservice.Core;
using Microservice.Infrastructure.AspNetCore;
using Microservice.ReviewService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication()
    .AddModule<AspNetCoreModule>()
    .AddModule<ReviewServiceDomainModule>()
    .AddModule<ReviewServiceInfrastructureModule>()
    .AddModule<ReviewServiceApplicationContractsModule>()
    .AddModule<ReviewServiceApplicationModule>()
    .AddModule<ReviewServiceWebModule>()
    .Configure();

var app = builder.Build();
app.UseApplication();

app.Run();