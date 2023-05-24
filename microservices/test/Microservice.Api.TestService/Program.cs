using Microservice.AspNetCore;
using Microservice.Core;
using Microservice.TestService;

var builder = WebApplication.CreateBuilder(args);
builder.AddApplication<TestServiceApiModule>()
    .ConfigureServices();

var app = builder.Build();
app.UseApplication();

app.Run();
