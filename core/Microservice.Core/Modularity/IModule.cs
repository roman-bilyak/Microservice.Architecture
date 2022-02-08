using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core.Modularity;

public interface IModule
{
    void Configure(IServiceCollection services);

    void Initialize(IServiceProvider serviceProvider);

    void Shutdown(IServiceProvider serviceProvider);
}