using Microservice.Core;
using Microservice.IdentityService;
using Microservice.Infrastructure.AspNetCore;
using Microservice.Infrastructure.AspNetCore.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddApplication()
    .AddModule<AspNetCoreModule>()
    .AddModule<IdentityServiceDomainModule>()
    .AddModule<IdentityServiceInfrastructureModule > ()
    .AddModule<IdentityServiceApplicationContractsModule>()
    .AddModule<IdentityServiceApplicationModule>()
    .AddModule<IdentityServiceWebModule>()
    .ConfigureServices();

var app = builder.Build();
app.UseApplication();

app.Run();
