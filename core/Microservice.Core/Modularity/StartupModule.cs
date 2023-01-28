using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core.Modularity;

public abstract class StartupModule : IStartupModule
{
    public virtual void PreConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    public virtual void PostConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    public virtual void PreConfigure(IServiceProvider serviceProvider)
    {
    }

    public virtual void Configure(IServiceProvider serviceProvider)
    {
    }

    public virtual void PostConfigure(IServiceProvider serviceProvider)
    {
    }

    public virtual void Shutdown(IServiceProvider serviceProvider)
    {
    }
}