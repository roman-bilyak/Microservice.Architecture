using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core.Modularity;

public abstract class StartupModule : IStartupModule
{
    public virtual void PreConfigureServices(IServiceCollection services)
    {
    }

    public virtual void ConfigureServices(IServiceCollection services)
    {
    }

    public virtual void PostConfigureServices(IServiceCollection services)
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