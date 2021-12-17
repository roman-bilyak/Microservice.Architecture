using Microservice.AspNetCore;
using Microservice.Core;
using Microservice.Gateway;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication()
    .AddModule<AspNetCoreModule>()
    .AddModule<GatewayWebModule>()
    .Configure();

var app = builder.Build();
app.UseApplication();

app.Run();
