using Microservice.AspNetCore;
using Microservice.Core;
using Microservice.ProductService.Application;
using Microservice.ProductService.Domain;
using Microservice.ProductService.Host;
using Microservice.ProductService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication()
    .AddModule<AspNetCoreModule>()
    .AddModule<ProductServiceDomainModule>()
    .AddModule<ProductServiceInfrastructureModule>()
    .AddModule<ProductServiceApplicationModule>()
    .AddModule<ProductServiceHostModule>()
    .Configure();

var app = builder.Build();
app.UseApplication();

app.Run();