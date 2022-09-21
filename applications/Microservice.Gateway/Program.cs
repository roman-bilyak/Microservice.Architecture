using Microservice.AspNetCore;
using Microservice.Core;
using Microservice.Gateway;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplication<GatewayModule>()
    .ConfigureServices();

var app = builder.Build();
app.UseApplication();

app.Run();
