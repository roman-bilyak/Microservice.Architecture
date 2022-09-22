using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core.Modularity;

public interface IStartupModule
{
    void PreConfigureServices(IServiceCollection services);

    void ConfigureServices(IServiceCollection services);

    void PostConfigureServices(IServiceCollection services);

    void PreConfigure(IServiceProvider serviceProvider);

    void Configure(IServiceProvider serviceProvider);

    void PostConfigure(IServiceProvider serviceProvider);

    void Shutdown(IServiceProvider serviceProvider);
}