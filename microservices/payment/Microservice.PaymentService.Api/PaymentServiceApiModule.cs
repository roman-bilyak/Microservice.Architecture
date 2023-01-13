﻿using Microservice.Api;
using Microservice.Application;
using Microservice.AspNetCore;
using Microservice.Core.Modularity;

namespace Microservice.PaymentService;

[DependsOn<PaymentServiceApplicationModule>]
[DependsOn<PaymentServiceInfrastructureModule>]
[DependsOn<ApiModule>]
public sealed class PaymentServiceApiModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(PaymentServiceApplicationModule).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });
    }
}