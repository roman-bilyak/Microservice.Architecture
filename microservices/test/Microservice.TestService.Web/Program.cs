using Microservice.Core;
using Microservice.Infrastructure.AspNetCore;
using Microservice.Infrastructure.AspNetCore.Extensions;
using Microservice.TestService;

var builder = WebApplication.CreateBuilder(args);
builder.AddApplication()
    .AddModule<AspNetCoreModule>()
    .AddModule<TestServiceDomainModule>()
    .AddModule<TestServiceInfrastructureModule>()
    .AddModule<TestServiceApplicationContractsModule>()
    .AddModule<TestServiceApplicationModule>()
    .AddModule<TestServiceWebModule>()
    .ConfigureServices();

var app = builder.Build();
app.UseApplication();

app.Run();
