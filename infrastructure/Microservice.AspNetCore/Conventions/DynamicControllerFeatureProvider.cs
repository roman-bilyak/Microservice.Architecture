using Microservice.Core;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Microservice.AspNetCore.Conventions;

public class DynamicControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
{
    private readonly IApplication _application;

    public DynamicControllerFeatureProvider(IApplication application)
    {
        _application = application;
    }

    public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
    {
        foreach (IApplicationPartTypeProvider part in parts.OfType<IApplicationPartTypeProvider>())
        {
            foreach (TypeInfo type in part.Types)
            {
                if (IsController(type))
                {
                    TypeInfo? implementationType = _application.Services.GetImplementationType(type.AsType())?.GetTypeInfo();
                    TypeInfo controllerType = implementationType ?? type;
                    if (controllerType.IsClass
                        && !controllerType.IsAbstract
                        && !controllerType.ContainsGenericParameters
                        && !feature.Controllers.Contains(controllerType))
                    {
                        feature.Controllers.Add(controllerType);
                    }
                }
            }
        }
    }

    #region helper methods

    private bool IsController(TypeInfo typeInfo)
    {
        if (_application.ServiceProvider == null)
        {
            return false;
        }

        DynamicControllerOptions options = _application.ServiceProvider.GetRequiredService<IOptions<DynamicControllerOptions>>().Value;
        return options.IsController(typeInfo.AsType());
    }

    #endregion
}