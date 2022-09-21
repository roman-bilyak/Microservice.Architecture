namespace Microservice.Core.Helpers;

public static class TopologicalSort
{
    public static List<T> Sort<T>(IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies)
        where T : notnull
    {
        List<T> sorted = new List<T>();
        Dictionary<T, bool> visited = new Dictionary<T, bool>();

        foreach (var item in source)
        {
            Visit(item, getDependencies, sorted, visited);
        }

        return sorted;
    }

    #region helper methods

    private static void Visit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted, Dictionary<T, bool> visited)
        where T : notnull
    {
        bool inProcess;
        bool alreadyVisited = visited.TryGetValue(item, out inProcess);

        if (alreadyVisited)
        {
            if (inProcess)
            {
                throw new ArgumentException("Cyclic dependency found.");
            }
        }
        else
        {
            visited[item] = true;

            IEnumerable<T> dependencies = getDependencies(item);
            if (dependencies != null)
            {
                foreach (var dependency in dependencies)
                {
                    Visit(dependency, getDependencies, sorted, visited);
                }
            }

            visited[item] = false;
            sorted.Add(item);
        }
    }

    #endregion
}