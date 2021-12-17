using System.Reflection;

namespace Microservice.AspNetCore.Conventions;

public class DynamicControllerOptions
{
    private readonly IDictionary<Assembly, DynamicControllerSettings> _settings = new Dictionary<Assembly, DynamicControllerSettings>();

    public IReadOnlyList<Assembly> Assemblies => _settings.Select(x => x.Key).ToList();

    public void AddSettings(Assembly assembly, Func<Type, bool> typePredicate)
    {
        AddSettings(null, assembly, typePredicate);
    }

    public void AddSettings(string rootPath, Assembly assembly, Func<Type, bool> typePredicate)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        ArgumentNullException.ThrowIfNull(typePredicate);

        if (_settings.ContainsKey(assembly))
        {
            throw new ArgumentException($"Settings for specified assembly '{assembly.FullName}' already exist", nameof(assembly));
        }
        _settings.Add(assembly, new DynamicControllerSettings(typePredicate, rootPath));
    }

    public void SetRootPath(Assembly assembly, string rootPath)
    {
        if (!_settings.TryGetValue(assembly, out DynamicControllerSettings? settings))
        {
            throw new ArgumentException($"There are no settings for specified assembly '{assembly.FullName}'", nameof(assembly));
        }

        settings.RootPath = rootPath;
    }

    public bool IsController(Type type)
    {
        return _settings.Any(x => x.Key == type.Assembly && x.Value.TypePredicate(type));
    }

    public string GetRootPath(Type type)
    {
        if (_settings.TryGetValue(type.Assembly, out DynamicControllerSettings? settings))
        {
            return settings.RootPath;
        }
        return null;
    }
}