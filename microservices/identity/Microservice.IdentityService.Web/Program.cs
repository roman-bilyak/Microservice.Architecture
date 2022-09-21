using Microservice.AspNetCore;
using Microservice.Core;
using Microservice.IdentityService;

var builder = WebApplication.CreateBuilder(args);
builder.AddApplication<IdentityServiceWebModule>()
    .ConfigureServices();

var app = builder.Build();
app.UseApplication();

app.Run();
