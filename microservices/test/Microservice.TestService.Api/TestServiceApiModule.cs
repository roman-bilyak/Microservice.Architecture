using MassTransit;
using Microservice.Api;
using Microservice.Application;
using Microservice.AspNetCore;
using Microservice.Core.Modularity;
using Microservice.TestService.Tests;

namespace Microservice.TestService;

[DependsOn<TestServiceApplicationModule>]
[DependsOn<TestServiceInfrastructureModule>]
[DependsOn<ApiModule>]
public sealed class TestServiceApiModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddMassTransit(x =>
        {
            x.AddConsumer<TestMessageConsumer>();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                string host = configuration.GetValue<string>("RabbitMq:Host") ?? "localhost";
                ushort port = configuration.GetValue<ushort>("RabbitMq:Port");
                string virtualHost = configuration.GetValue<string>("RabbitMq:VirtualHost") ?? "/";
                string userName = configuration.GetValue<string>("RabbitMq:UserName") ?? "guest";
                string password = configuration.GetValue<string>("RabbitMq:Password") ?? "guest";

                cfg.Host(host, port, virtualHost, h =>
                {
                    h.Username(userName);
                    h.Password(password);
                });

                cfg.ConfigureEndpoints(ctx);
            });
        });

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(TestServiceApplicationModule).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });
    }
}