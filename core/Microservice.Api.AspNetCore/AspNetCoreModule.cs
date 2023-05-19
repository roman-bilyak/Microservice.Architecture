﻿using Microservice.Api.AspNetCore.Security;
using Microservice.Core;
using Microservice.Core.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microservice.AspNetCore;

public sealed class AspNetCoreModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddWrappedService<IApplicationBuilder>();
        services.AddTransient<DynamicControllerConvention>();
        services.AddTransient<DynamicControllerFeatureProvider>();

        services.AddTransient<ICurrentPrincipleAccessor, HttpContextCurrentPrincipleAccessor>();
        services.AddHttpContextAccessor();

        //TODO: move list of allowed origins into config file
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(x =>
            {
                x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });

        services.AddMvc(x =>
        {
            x.Filters.Add<ExceptionActionFilter>();
        });
    }

    public override void PreConfigure(IServiceProvider serviceProvider)
    {
        base.PreConfigure(serviceProvider);
        
        string pathBase = serviceProvider.GetRequiredService<IConfiguration>().GetValue<string>("PathBase") ?? string.Empty;
        IApplicationBuilder app = serviceProvider.GetApplicationBuilder();
        app.UsePathBase(pathBase);

        MvcOptions mvcOptions = serviceProvider.GetOptions<MvcOptions>();
        mvcOptions.Conventions.Add(serviceProvider.GetRequiredService<DynamicControllerConvention>());

        ApplicationPartManager applicationPartManager = serviceProvider.GetRequiredService<ApplicationPartManager>();
        applicationPartManager.FeatureProviders.Add(serviceProvider.GetRequiredService<DynamicControllerFeatureProvider>());

        DynamicControllerOptions dynamicControllerOptions = serviceProvider.GetOptions<DynamicControllerOptions>();
        foreach (Assembly assembly in dynamicControllerOptions.Assemblies)
        {
            applicationPartManager.ApplicationParts.Add(new AssemblyPart(assembly));
        }
    }

    public override void Configure(IServiceProvider serviceProvider)
    {
        base.Configure(serviceProvider);

        IApplicationBuilder app = serviceProvider.GetApplicationBuilder();
        app.UseRouting();
        app.UseCors();
    }

    public override void PostConfigure(IServiceProvider serviceProvider)
    {
        base.PostConfigure(serviceProvider);

        IApplicationBuilder app = serviceProvider.GetApplicationBuilder();
        app.UseEndpoints(x => x.MapDefaultControllerRoute());
    }
}