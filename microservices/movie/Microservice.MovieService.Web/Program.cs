using Microservice.AspNetCore;
using Microservice.Core;
using Microservice.MovieService;

var builder = WebApplication.CreateBuilder(args);
builder.AddApplication<MovieServiceWebModule>()
    .ConfigureServices();

var app = builder.Build();
app.UseApplication();

app.Run();