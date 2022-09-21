using Microservice.AspNetCore;
using Microservice.Core;
using Microservice.TestService;

var builder = WebApplication.CreateBuilder(args);
builder.AddApplication<TestServiceWebModule>()
    .ConfigureServices();

var app = builder.Build();
app.UseApplication();

app.Run();
