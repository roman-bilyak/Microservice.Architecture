using Microservice.Core;
using Microservice.Infrastructure.AspNetCore;
using Microservice.IdentityService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication()
    .AddModule<AspNetCoreModule>()
    .AddModule<IdentityServiceDomainModule>()
    .AddModule<IdentityServiceInfrastructureModule > ()
    .AddModule<IdentityServiceApplicationContractsModule>()
    .AddModule<IdentityServiceApplicationModule>()
    .AddModule<IdentityServiceWebModule>()
    .Configure();

var app = builder.Build();
app.UseApplication();

app.Run();
