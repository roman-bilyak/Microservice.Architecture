using Microservice.Core.Helpers;

namespace Microservice.Core.Modularity;

internal static class StartupModuleHelper
{
    public static List<IStartupModule> GetModules<TStartupModule>()
        where TStartupModule : class, IStartupModule, new()
    {
        List<IStartupModule> modules = new List<IStartupModule>();
        List<Type> moduleTypes = TopologicalSort.Sort(new[] { typeof(TStartupModule) }, GetDependencies);
        foreach (Type moduleType in moduleTypes)
        {
            IStartupModule? startupModule = Activator.CreateInstance(moduleType) as IStartupModule;
            if (startupModule is not null)
            {
                modules.Add(startupModule);
            }
        }
        return modules;
    }

    #region helper methods

    private static List<Type> GetDependencies(Type type)
    {
        return type
            .GetCustomAttributes(true)
            .OfType<DependsOnAttribute>()
            .Select(x => x.Type)
            .ToList();
    }

    #endregion
}