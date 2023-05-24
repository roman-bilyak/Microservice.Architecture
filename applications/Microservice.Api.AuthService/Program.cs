using Microservice.AspNetCore;
using Microservice.AuthService;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplication<AuthServiceApiModule>()
    .ConfigureServices();

var app = builder.Build();
app.UseApplication();

app.Run();