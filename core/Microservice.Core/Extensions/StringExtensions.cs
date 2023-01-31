using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Microservice.Core.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? value)
    {
        return string.IsNullOrEmpty(value);
    }

    [return: NotNullIfNotNull(nameof(value))]
    public static string? RemovePrefix(this string? value, string? prefix)
    {
        if (!value.IsNullOrEmpty() && !prefix.IsNullOrEmpty() 
            && value.StartsWith(prefix))
        {
            return value[prefix.Length..];
        }
        return value;
    }

    [return: NotNullIfNotNull(nameof(value))]
    public static string? RemoveSuffix(this string? value, string? suffix)
    {
        if (!value.IsNullOrEmpty() && !suffix.IsNullOrEmpty() 
            && value.EndsWith(suffix))
        {
            return value[..^suffix.Length];
        }
        return value;
    }

    public static string? FindPrefix(this string? value, params string[] prefixes)
    {
        if (value is null)
        {
            return null;
        }

        foreach (var prefix in prefixes)
        {
            if (value.StartsWith(prefix))
            {
                return prefix;
            }
        }

        return null;
    }

    [return: NotNullIfNotNull(nameof(value))]
    public static string? Pluralize(this string? value)
    {
        if (value is null)
        {
            return null;
        }

        if (value.EndsWith("y", true, CultureInfo.InvariantCulture))
        {
            return value.Remove(value.Length - 1, 1) + "ies";
        }

        if (value.EndsWith("s", true, CultureInfo.InvariantCulture)
            || value.EndsWith("x", true, CultureInfo.InvariantCulture)
            || value.EndsWith("z", true, CultureInfo.InvariantCulture)
            || value.EndsWith("ch", true, CultureInfo.InvariantCulture)
            || value.EndsWith("sh", true, CultureInfo.InvariantCulture))
        {
            return value + "es";
        }

        return value + "s";
    }
}