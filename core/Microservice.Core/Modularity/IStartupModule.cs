using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core.Modularity;

public interface IStartupModule
{
    void PreConfigureServices(IServiceCollection services, IConfiguration configuration);

    void ConfigureServices(IServiceCollection services, IConfiguration configuration);

    void PostConfigureServices(IServiceCollection services, IConfiguration configuration);

    void PreConfigure(IServiceProvider serviceProvider);

    void Configure(IServiceProvider serviceProvider);

    void PostConfigure(IServiceProvider serviceProvider);

    void Shutdown(IServiceProvider serviceProvider);
}