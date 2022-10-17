using MassTransit;
using Microservice.Api;
using Microservice.Core.Modularity;
using Microservice.TestService.Tests;

namespace Microservice.TestService;

[DependsOn(typeof(TestServiceApplicationModule))]
[DependsOn(typeof(TestServiceInfrastructureModule))]
[DependsOn(typeof(ApiModule))]
public sealed class TestServiceWebModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddMassTransit(x =>
        {
            x.AddConsumer<TestMessageConsumer>();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ConfigureEndpoints(ctx);
            });
        });
    }
}