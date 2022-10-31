using System.Reflection;

namespace Microservice.AspNetCore;

public class DynamicControllerOptions
{
    private readonly IDictionary<Assembly, DynamicControllerSettings> _settings = new Dictionary<Assembly, DynamicControllerSettings>();

    public IReadOnlyList<Assembly> Assemblies => _settings.Select(x => x.Key).ToList();

    public void AddSettings(Assembly assembly, Func<Type, bool> typePredicate)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        ArgumentNullException.ThrowIfNull(typePredicate);

        if (_settings.ContainsKey(assembly))
        {
            throw new ArgumentException($"Settings for specified assembly '{assembly.FullName}' already exist", nameof(assembly));
        }
        _settings.Add(assembly, new DynamicControllerSettings(typePredicate));
    }

    public bool IsController(Type type)
    {
        List<Assembly> assemblies = GetAssemblies(type);
        return _settings.Any(x => assemblies.Contains(x.Key) && x.Value.TypePredicate(type));
    }

    #region helper methods
    private static List<Assembly> GetAssemblies(Type type)
    {
        return GetAllTypes(type).Select(x => x.Assembly).Distinct().ToList();
    }

    private static IEnumerable<Type> GetAllTypes(Type type)
    {
        if (type == null)
        {
            yield break;
        }

        yield return type;

        foreach (var i in type.GetInterfaces())
        {
            yield return i;
        }

        Type? currentBaseType = type.BaseType;
        while (currentBaseType != null)
        {
            yield return currentBaseType;
            currentBaseType = currentBaseType.BaseType;
        }
    }

    #endregion
}