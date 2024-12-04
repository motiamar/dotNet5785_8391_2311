using System.Runtime.CompilerServices;

namespace Helpers;

internal static class Tools
{
    /// <summary>
    /// func to ovveride and print tne logical entities
    /// </summary>
    /// <typeparam name="T">logic entity</typeparam>
    /// <returns> a string of all the prperty</returns>
    public static string TostringProperty<T>(this T t)
    {
        if (t == null)
            return "Null";
        var properties = typeof(T).GetProperties();
        var result = new System.Text.StringBuilder();
        foreach ( var prop in properties )
        {
            var value = prop.GetValue(t, null);
            result.AppendLine($"{ prop.Name}:{value}");
        }
         return result.ToString();
    }

}
