using Microservice.Core;
using Microservice.Infrastructure.AspNetCore;
using Microservice.TestService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication()
    .AddModule<AspNetCoreModule>()
    .AddModule<TestServiceDomainModule>()
    .AddModule<TestServiceInfrastructureModule>()
    .AddModule<TestServiceApplicationContractsModule>()
    .AddModule<TestServiceApplicationModule>()
    .AddModule<TestServiceWebModule>()
    .Configure();

var app = builder.Build();
app.UseApplication();

app.Run();
