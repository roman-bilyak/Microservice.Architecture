using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core.CQRS
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCQRS(this IServiceCollection services, params Type[] handlerAssemblyMarkerTypes)
        {
            return services.AddMediatR(handlerAssemblyMarkerTypes);
        }
    }
}