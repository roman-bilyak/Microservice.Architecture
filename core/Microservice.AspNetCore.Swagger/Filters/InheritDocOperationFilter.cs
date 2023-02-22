using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Xml;

namespace Microservice.AspNetCore.Swagger;

internal class InheritDocOperationFilter : IOperationFilter
{
    private readonly IMemoryCache _memoryCache;

    public InheritDocOperationFilter(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        MethodInfo methodInfo = context.MethodInfo;

        if (operation.Summary is null)
        {
            string summary = GetSummary(methodInfo);
            if (!string.IsNullOrEmpty(summary))
            {
                operation.Summary = summary;
            }
        }

        ParameterInfo[] parameters = methodInfo.GetParameters();
        foreach (OpenApiParameter parameter in operation.Parameters)
        {
            if (parameter.Description is not null)
            {
                continue;
            }

            ParameterInfo? parameterInfo = parameters.FirstOrDefault(x => x.Name == parameter.Name);
            if (parameterInfo is not null)
            {
                string description = GetDescription(parameterInfo);
                if (!string.IsNullOrEmpty(description))
                {
                    parameter.Description = description;
                }
            }
        }
    }

    #region helper methods

    private string GetSummary(MethodInfo methodInfo)
    {
        string summary = GetSummaryFromXml(methodInfo);
        if (string.IsNullOrEmpty(summary))
        {
            summary = GetSummaryFromInheritedMethod(methodInfo);
        }
        return summary;
    }

    private string GetDescription(ParameterInfo parameterInfo)
    {
        string description = GetDescriptionFromXml(parameterInfo);
        if (string.IsNullOrEmpty(description))
        {
            description = GetDescriptionFromInheritedParameter(parameterInfo);
        }
        return description;
    }

    private string GetSummaryFromXml(MemberInfo memberInfo)
    {
        XmlNode? memberNode = GetMemberNodeFromXml(memberInfo);
        if (memberNode is not null)
        {
            XmlNode? summaryNode = memberNode.SelectSingleNode("summary");
            if (summaryNode is not null)
            {
                return summaryNode.InnerText.Trim();
            }
        }

        return string.Empty;
    }

    private string GetDescriptionFromXml(ParameterInfo parameterInfo)
    {
        XmlNode? memberNode = GetMemberNodeFromXml(parameterInfo.Member);
        if (memberNode is not null)
        {
            XmlNode? paramNode = memberNode.SelectSingleNode($"//param[@name='{parameterInfo.Name}']"); ;
            if (paramNode is not null)
            {
                return paramNode.InnerText.Trim();
            }
        }

        return string.Empty;
    }

    private XmlNode? GetMemberNodeFromXml(MemberInfo memberInfo)
    {
        Type? declaringType = memberInfo.DeclaringType;
        if (declaringType is null)
        {
            return null;
        }

        Assembly declaringAssembly = declaringType.Assembly;
        string xmlCommentsPath = Path.ChangeExtension(declaringAssembly.Location, ".xml");
        if (!_memoryCache.TryGetValue(xmlCommentsPath, out XmlDocument? xmlComments))
        {
            xmlComments = new XmlDocument();
            try
            {
                xmlComments.Load(xmlCommentsPath);
            }
            catch
            {
            }

            _memoryCache.Set(xmlCommentsPath, xmlComments, TimeSpan.FromMinutes(5));
        }

        if (xmlComments is null)
        {
            return null;
        }

        return xmlComments.SelectSingleNode($"//member[@name='{GetMemberName(memberInfo)}']");
    }

    private string GetSummaryFromInheritedMethod(MethodInfo methodInfo)
    {
        MethodInfo baseMethodInfo = methodInfo.GetBaseDefinition();
        if (baseMethodInfo is not null && baseMethodInfo != methodInfo)
        {
            string summary = GetSummary(baseMethodInfo);
            if (!string.IsNullOrEmpty(summary))
            {
                return summary;
            }
        }

        if (methodInfo.DeclaringType is null)
        {
            return string.Empty;
        }

        foreach (Type interfaceType in methodInfo.DeclaringType.GetInterfaces().Reverse())
        {
            MethodInfo? interfaceMethodInfo = interfaceType.GetMethod(methodInfo.Name, methodInfo.GetParameters().Select(x => x.ParameterType).ToArray());
            if (interfaceMethodInfo is not null)
            {
                string interfaceSummary = GetSummary(interfaceMethodInfo);
                if (!string.IsNullOrEmpty(interfaceSummary))
                {
                    return interfaceSummary;
                }
            }
        }

        return string.Empty;
    }

    private string GetDescriptionFromInheritedParameter(ParameterInfo parameterInfo)
    {
        MethodInfo methodInfo = (MethodInfo)parameterInfo.Member;
        MethodInfo baseMethodInfo = methodInfo.GetBaseDefinition();

        if (baseMethodInfo is not null && baseMethodInfo != methodInfo)
        {
            ParameterInfo? baseParameterInfo = baseMethodInfo.GetParameters()
                .FirstOrDefault(x => x.Name == parameterInfo.Name);

            if (baseParameterInfo is not null)
            {
                string description = GetDescription(baseParameterInfo);
                if (!string.IsNullOrEmpty(description))
                {
                    return description;
                }
            }
        }

        if (methodInfo.DeclaringType is null)
        {
            return string.Empty;
        }

        foreach (Type interfaceType in methodInfo.DeclaringType.GetInterfaces().Reverse())
        {
            MethodInfo? interfaceMethodInfo = interfaceType.GetMethod(methodInfo.Name, methodInfo.GetParameters().Select(x => x.ParameterType).ToArray());
            if (interfaceMethodInfo is not null)
            {
                ParameterInfo? interfaceParameterInfo = interfaceMethodInfo.GetParameters()
                    .FirstOrDefault(x => x.Name == parameterInfo.Name);

                if (interfaceParameterInfo is not null)
                {
                    string interfaceDescription = GetDescription(interfaceParameterInfo);
                    if (!string.IsNullOrEmpty(interfaceDescription))
                    {
                        return interfaceDescription;
                    }
                }
            }
        }

        return string.Empty;
    }


    private static string GetMemberName(MemberInfo memberInfo)
    {
        if (memberInfo is Type type)
        {
            return $"T:{type.FullName}";
        }
        if (memberInfo is MethodInfo methodInfo)
        {
            return $"M:{methodInfo.DeclaringType?.FullName}.{methodInfo.Name}({string.Join(",", methodInfo.GetParameters().Select(x => x.ParameterType.FullName))})";
        }
        if (memberInfo is PropertyInfo propertyInfo)
        {
            return $"P:{propertyInfo.DeclaringType?.FullName}.{propertyInfo.Name}";
        }
        if (memberInfo is FieldInfo fieldInfo)
        {
            return $"F:{fieldInfo.DeclaringType?.FullName}.{fieldInfo.Name}";
        }
        if (memberInfo is EventInfo eventInfo)
        {
            return $"E:{eventInfo.DeclaringType?.FullName}.{eventInfo.Name}";
        }
        if (memberInfo is ConstructorInfo constructorInfo)
        {
            return $"M:{constructorInfo.DeclaringType?.FullName}.#ctor";
        }

        throw new ArgumentException($"Cannot generate member name for type {memberInfo.GetType().FullName}");
    }

    #endregion
}