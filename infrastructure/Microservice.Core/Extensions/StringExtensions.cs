namespace Microservice.Core.Extensions;

public static class StringExtensions
{
    public static string RemoveSuffix(this string s, string suffix)
    {
        if (s.EndsWith(suffix))
        {
            return s.Substring(0, s.Length - suffix.Length);
        }
        return s;
    }
}