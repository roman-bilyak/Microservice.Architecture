using Microservice.Core;
using Microservice.Infrastructure.AspNetCore;
using Microservice.Infrastructure.AspNetCore.Extensions;
using Microservice.ReviewService;

var builder = WebApplication.CreateBuilder(args);
builder.AddApplication()
    .AddModule<AspNetCoreModule>()
    .AddModule<ReviewServiceDomainModule>()
    .AddModule<ReviewServiceInfrastructureModule>()
    .AddModule<ReviewServiceApplicationContractsModule>()
    .AddModule<ReviewServiceApplicationModule>()
    .AddModule<ReviewServiceWebModule>()
    .ConfigureServices();

var app = builder.Build();
app.UseApplication();

app.Run();