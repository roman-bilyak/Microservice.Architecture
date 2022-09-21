using Microservice.AspNetCore;
using Microservice.Core;
using Microservice.ReviewService;

var builder = WebApplication.CreateBuilder(args);
builder.AddApplication<ReviewServiceWebModule>()
    .ConfigureServices();

var app = builder.Build();
app.UseApplication();

app.Run();