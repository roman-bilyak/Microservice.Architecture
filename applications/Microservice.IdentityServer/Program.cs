using Microservice.AspNetCore;
using Microservice.IdentityServer;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplication<IdentityServerModule>()
    .ConfigureServices();

var app = builder.Build();
app.UseApplication();

app.Run();