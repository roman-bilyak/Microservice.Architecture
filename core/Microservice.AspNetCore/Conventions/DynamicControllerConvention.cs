using Microservice.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Microservice.AspNetCore;

public class DynamicControllerConvention : IApplicationModelConvention
{
    private readonly DynamicControllerOptions _options;

    public DynamicControllerConvention(IOptions<DynamicControllerOptions> options)
    {
        _options = options.Value;
    }

    public void Apply(ApplicationModel application)
    {
        foreach (ControllerModel controller in application.Controllers)
        {
            Type controllerType = controller.ControllerType.AsType();
            if (!_options.IsController(controllerType))
            {
                continue;
            }

            NormalizeName(controller);
            SetGroupName(controller);
            SetVisibility(controller);
            SetRoute(controller);

            foreach (ActionModel action in controller.Actions)
            {
                ConfigureAction(controller, action);
            }
        }
    }

    #region helper methods

    private static void NormalizeName(ControllerModel controller)
    {
        string controllerName = controller.ControllerType.AsType().Name;
        if (controllerName.StartsWith("I") && controllerName.Length > 1 && char.IsUpper(controllerName[1]))
        {
            controllerName = controllerName.Substring(1, controllerName.Length - 1);
        }
        controller.ControllerName = controllerName
            .RemoveSuffix("Proxy")
            .RemoveSuffix("Controller")
            .RemoveSuffix("ApplicationService");
    }

    private static void SetGroupName(ControllerModel controller)
    {
        if (!string.IsNullOrEmpty(controller.ApiExplorer.GroupName))
        {
            return;
        }
        controller.ApiExplorer.GroupName = controller.ControllerName;
    }

    private static void SetVisibility(ControllerModel controller)
    {
        if (controller.ApiExplorer.IsVisible.HasValue)
        {
            return;
        }
        controller.ApiExplorer.IsVisible = true;
    }

    private static void SetRoute(ControllerModel controller)
    {
        if (!controller.Selectors.Any())
        {
            controller.Selectors.Add(new SelectorModel());
        }

        foreach (var selector in controller.Selectors)
        {
            if (selector.AttributeRouteModel is null)
            {
                selector.AttributeRouteModel = new AttributeRouteModel(new RouteAttribute("api"));
            }
        }
    }

    private static void ConfigureAction(ControllerModel controller, ActionModel action)
    {
        NormalizeName(action);
        SetVisibility(action);
        SetRoute(action);
        ConfigureParameters(action);
    }

    private static void NormalizeName(ActionModel action)
    {
        string actionName = action.ActionName.RemoveSuffix("Async");
        action.ActionName = actionName;
    }

    private static void SetVisibility(ActionModel action)
    {
        if (action.ApiExplorer.IsVisible.HasValue)
        {
            return;
        }

        string actionName = action.ActionName;
        action.ApiExplorer.IsVisible = actionName != "DynProxyGetTarget"
            && actionName != "DynProxySetTarget"
            && actionName != "GetInterceptors"; ;
    }

    private static void SetRoute(ActionModel action)
    {
        if (!action.Selectors.Any())
        {
            action.Selectors.Add(new SelectorModel());
        }

        foreach (var selector in action.Selectors)
        {
            string? httpMethod = selector.ActionConstraints
                .OfType<HttpMethodActionConstraint>()
                .FirstOrDefault()?
                .HttpMethods?
                .FirstOrDefault();

            if (httpMethod is null)
            {
                httpMethod = GetHttpMethodByActionName(action);
            }

            if (selector.AttributeRouteModel is null)
            {
                string actionUrlTemplate = GetActionUrlTemplate(action);
                selector.AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(actionUrlTemplate));
            }

            if (!selector.ActionConstraints.OfType<HttpMethodActionConstraint>().Any())
            {
                selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { httpMethod }));
            }
        }
    }

    private static string GetHttpMethodByActionName(ActionModel action)
    {
        string actionName = action.ActionName;
        return actionName switch
        {
            _ when actionName.StartsWith("Get", StringComparison.InvariantCultureIgnoreCase) => "GET",
            _ when actionName.StartsWith("Create", StringComparison.InvariantCultureIgnoreCase) => "POST",
            _ when actionName.StartsWith("Update", StringComparison.InvariantCultureIgnoreCase) => "PUT",
            _ when actionName.StartsWith("Delete", StringComparison.InvariantCultureIgnoreCase) => "DELETE",
            _ => "POST"
        };
    }

    private static string GetActionUrlTemplate(ActionModel action)
    {
        string actionUrlTemplate = action.Controller.ControllerName;

        ParameterModel? idParameterModel = action.Parameters.FirstOrDefault(x => x.ParameterName == "id");
        if (idParameterModel is not null && IsPrimitiveType(idParameterModel.ParameterType))
        {
            actionUrlTemplate += "/{id}";
        }

        string actionName = action.ActionName
            .RemovePrefix("Get")
            .RemovePrefix("List")
            .RemovePrefix("Create")
            .RemovePrefix("Update")
            .RemovePrefix("Delete");

        if (!actionName.IsNullOrEmpty())
        {
            actionUrlTemplate += $"/{actionName}";
        }

        return actionUrlTemplate;
    }

    private static void ConfigureParameters(ActionModel action)
    {
        var httpMethods = action.Selectors
            .SelectMany(x => x.ActionConstraints.OfType<HttpMethodActionConstraint>())?
            .SelectMany(x => x.HttpMethods) ?? new string[] { };

        bool useFormBodyBinding = !httpMethods.Contains("GET")
            && !httpMethods.Contains("DELETE");

        foreach (ParameterModel parameter in action.Parameters)
        {
            if (useFormBodyBinding
                && !IsPrimitiveType(parameter.ParameterInfo.ParameterType)
                && !typeof(CancellationToken).IsAssignableFrom(parameter.ParameterInfo.ParameterType))
            {
                parameter.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
            }
        }
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