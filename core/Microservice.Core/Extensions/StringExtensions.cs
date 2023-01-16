namespace Microservice.Core.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }

    public static string RemovePrefix(this string str, string prefix)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(prefix, nameof(prefix));

        if (!str.IsNullOrEmpty() && str.StartsWith(prefix))
        {
            return str[prefix.Length..];
        }
        return str;
    }

    public static string RemoveSuffix(this string str, string suffix)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(suffix, nameof(suffix));

        if (!str.IsNullOrEmpty() && str.EndsWith(suffix))
        {
            return str[..^suffix.Length];
        }
        return str;
    }
}