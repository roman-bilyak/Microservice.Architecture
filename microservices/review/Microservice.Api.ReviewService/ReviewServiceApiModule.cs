﻿using Microservice.Api;
using Microservice.Application;
using Microservice.AspNetCore;
using Microservice.Core.Modularity;

namespace Microservice.ReviewService;

[DependsOn<ReviewServiceApplicationModule>]
[DependsOn<ReviewServiceInfrastructureModule>]
[DependsOn<ApiModule>]
public sealed class ReviewServiceApiModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.Configure<DynamicControllerOptions>(options =>
        {
            options.AddSettings(typeof(ReviewServiceApplicationModule).Assembly,
                x => typeof(IApplicationService).IsAssignableFrom(x));
        });
    }
}