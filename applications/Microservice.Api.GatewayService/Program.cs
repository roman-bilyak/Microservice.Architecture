using Microservice.AspNetCore;
using Microservice.Core;
using Microservice.GatewayService;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplication<GatewayServiceApiModule>()
    .ConfigureServices();

var app = builder.Build();
app.UseApplication();

app.Run();
