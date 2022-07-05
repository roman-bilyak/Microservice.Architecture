using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microservice.Core.CQRS
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCQRS(this IServiceCollection services, params Assembly[] assemblies)
        {
            return services.AddMediatR(assemblies);
        }
    }
}