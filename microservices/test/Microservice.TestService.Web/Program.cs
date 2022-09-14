using Microservice.AspNetCore;
using Microservice.AspNetCore.Swagger;
using Microservice.Core;
using Microservice.TestService;

var builder = WebApplication.CreateBuilder(args);
builder.AddApplication()
    .AddModule<AspNetCoreModule>()
    .AddModule<SwaggerModule>()
    .AddModule<TestServiceDomainModule>()
    .AddModule<TestServiceInfrastructureModule>()
    .AddModule<TestServiceApplicationContractsModule>()
    .AddModule<TestServiceApplicationModule>()
    .AddModule<TestServiceWebModule>()
    .ConfigureServices();

var app = builder.Build();
app.UseApplication();

app.Run();
