using Microservice.AspNetCore;
using Microservice.AspNetCore.Swagger;
using Microservice.Core;
using Microservice.PaymentService;

var builder = WebApplication.CreateBuilder(args);
builder.AddApplication()
    .AddModule<AspNetCoreModule>()
    .AddModule<SwaggerModule>()
    .AddModule<PaymentServiceDomainModule>()
    .AddModule<PaymentServiceInfrastructureModule>()
    .AddModule<PaymentServiceApplicationContractsModule>()
    .AddModule<PaymentServiceApplicationModule>()
    .AddModule<PaymentServiceWebModule>()
    .ConfigureServices();

var app = builder.Build();
app.UseApplication();

app.Run();
