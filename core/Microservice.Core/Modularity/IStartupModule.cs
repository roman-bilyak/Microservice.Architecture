using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core.Modularity;

public interface IStartupModule
{
    void ConfigureServices(IServiceCollection services);

    void Configure(IServiceProvider serviceProvider);

    void Shutdown(IServiceProvider serviceProvider);
}