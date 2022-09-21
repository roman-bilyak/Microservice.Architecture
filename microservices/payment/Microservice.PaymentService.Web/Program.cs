using Microservice.AspNetCore;
using Microservice.Core;
using Microservice.PaymentService;

var builder = WebApplication.CreateBuilder(args);
builder.AddApplication<PaymentServiceWebModule>()
    .ConfigureServices();

var app = builder.Build();
app.UseApplication();

app.Run();
