using Microservice.AspNetCore;
using Microservice.Core;
using Microservice.ReviewService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication()
    .AddModule<AspNetCoreModule>()
    .AddModule<ReviewServiceDomainModule>()
    .AddModule<ReviewServiceInfrastructureModule>()
    .AddModule<ReviewServiceApplicationModule>()
    .AddModule<ReviewServiceWebModule>()
    .Configure();

var app = builder.Build();
app.UseApplication();

app.Run();