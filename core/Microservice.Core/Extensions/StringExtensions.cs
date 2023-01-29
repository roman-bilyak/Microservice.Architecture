using System.Diagnostics.CodeAnalysis;

namespace Microservice.Core.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? value)
    {
        return string.IsNullOrEmpty(value);
    }

    [return: NotNullIfNotNull(nameof(value))]
    public static string? RemovePrefix(this string? value, string prefix)
    {
        if (!value.IsNullOrEmpty() && !prefix.IsNullOrEmpty() 
            && value.StartsWith(prefix))
        {
            return value[prefix.Length..];
        }
        return value;
    }

    [return: NotNullIfNotNull(nameof(value))]
    public static string? RemoveSuffix(this string? value, string suffix)
    {
        if (!value.IsNullOrEmpty() && !suffix.IsNullOrEmpty() 
            && value.EndsWith(suffix))
        {
            return value[..^suffix.Length];
        }
        return value;
    }
}