namespace Microservice.Core.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }

    public static string RemovePrefix(this string str, string prefix)
    {
        if (prefix.IsNullOrEmpty() && str.StartsWith(prefix))
        {
            return str.Substring(prefix.Length, str.Length - prefix.Length);
        }
        return str;
    }

    public static string RemoveSuffix(this string str, string suffix)
    {
        if (str.EndsWith(suffix))
        {
            return str.Substring(0, str.Length - suffix.Length);
        }
        return str;
    }
}