using Microservice.Core;
using Microservice.Core.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.AspNetCore;

public sealed class AspNetCoreModule : Module
{
    public override void Configure(IServiceCollection services)
    {
        base.Configure(services);

        services.AddWrappedService<IApplicationBuilder>();
    }
}