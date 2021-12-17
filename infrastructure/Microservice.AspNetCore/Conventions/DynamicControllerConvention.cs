using Microservice.Core;
using Microservice.Core.Extensions;
using Microservice.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Microservice.AspNetCore.Conventions;

public class DynamicControllerConvention : IApplicationModelConvention
{
    public void Apply(ApplicationModel application)
    {
        foreach (ControllerModel controller in application.Controllers)
        {
            Type controllerType = controller.ControllerType.AsType();
                controller.ControllerName = controllerType.Name.RemoveSuffix("ApplicationService");
                controller.ApiExplorer.GroupName = controller.ControllerName;
                controller.ApiExplorer.IsVisible = true;

            foreach (ActionModel action in controller.Actions)
            {
                action.ActionName = action.ActionName.RemoveSuffix("Async");
                action.ApiExplorer.IsVisible = true;

                action.Selectors.Where(x => x.AttributeRouteModel == null).ToList()
                    .ForEach(s => action.Selectors.Remove(s));

                SelectorModel selector = new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel(new RouteAttribute($"api/{controller.ControllerName}/{action.ActionName}")),
                };

                string methodVerb = GetMethodVerbByActionName(action.ActionName);
                selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { methodVerb }));
                action.Selectors.Add(selector);

                foreach (ParameterModel parameter in action.Parameters)
                {
                    if (methodVerb != "GET" && methodVerb != "DELETE"
                        && !IsPrimitiveType(parameter.ParameterInfo.ParameterType)
                        && !typeof(CancellationToken).IsAssignableFrom(parameter.ParameterInfo.ParameterType))
                    {
                        parameter.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
                    }
                }
            }
        }
    }


    #region helper methods

    public static string GetMethodVerbByActionName(string actionName)
    {
        if (actionName.StartsWith("Get", StringComparison.OrdinalIgnoreCase))
        {
            return "GET";
        }

        if (actionName.StartsWith("Create", StringComparison.OrdinalIgnoreCase))
        {
            return "POST";
        }

        if (actionName.StartsWith("Update", StringComparison.OrdinalIgnoreCase))
        {
            return "PUT";
        }

        if (actionName.StartsWith("Delete", StringComparison.OrdinalIgnoreCase))
        {
            return "DELETE";
        }

        return "POST";
    }

    private static bool IsPrimitiveType(Type type)
    {
        return type.GetTypeInfo().IsPrimitive
            || type.GetTypeInfo().IsEnum
            || type == typeof(string)
            || type == typeof(decimal)
            || type == typeof(DateTime)
            || type == typeof(DateTimeOffset)
            || type == typeof(TimeSpan)
            || type == typeof(Guid);
    }

    #endregion
}