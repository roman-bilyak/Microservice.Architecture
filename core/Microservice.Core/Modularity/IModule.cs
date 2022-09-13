using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core.Modularity;

public interface IModule
{
    IConfiguration Configuration { get; set; }
    
    void ConfigureServices(IServiceCollection services);

    void Configure(IServiceProvider serviceProvider);

    void Shutdown(IServiceProvider serviceProvider);
}