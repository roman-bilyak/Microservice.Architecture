using Microservice.AspNetCore;
using Microservice.Core;
using Microservice.ProductService.Host;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication()
    .AddModule<ProductServiceHostModule>()
    .AddModule<AspNetCoreModule>()
    .Configure();

var app = builder.Build();
app.UseApplication();

app.Run();