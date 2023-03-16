using Microservice.Core;
using Microservice.Core.Modularity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice;

public sealed class CoreModule : StartupModule
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        base.ConfigureServices(services, configuration);

        services.AddTransient<ICurrentUser, CurrentUser>();
        services.AddTransient<ICurrentPrincipleAccessor, CurrentPrincipleAccessor>();
    }
}
