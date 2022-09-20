using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core.Modularity;

public abstract class StartupModule : IStartupModule
{
    public IConfiguration Configuration { get; set; }

    public virtual void ConfigureServices(IServiceCollection services)
    {
    }

    public virtual void Configure(IServiceProvider serviceProvider)
    {
    }

    public virtual void Shutdown(IServiceProvider serviceProvider)
    {
    }
}