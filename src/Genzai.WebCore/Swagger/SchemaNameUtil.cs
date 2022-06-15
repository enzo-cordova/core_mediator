using System.ComponentModel;

namespace Genzai.WebCore.Swagger;

/// <summary>
/// Utility class for schema names
/// </summary>
public static class SchemaNameUtil
{
    /// <summary>
    /// Get name for current Type
    /// </summary>
    /// <param name="type">Type</param>
    /// <returns>Name of the attribute</returns>
    public static string GetSchemaName(Type type)
    {
        string result = type.Name;

        Type[] argumentTypes = type.GenericTypeArguments;
        DisplayNameAttribute displayNameAttribute = type.GetCustomAttributes(false).OfType<DisplayNameAttribute>().FirstOrDefault();
        if (displayNameAttribute != null)
        {
            return displayNameAttribute.DisplayName;
        }
        else if (argumentTypes != null && argumentTypes.Length == 1)
        {
            result = argumentTypes[0].Name + result;
        }

        result = result.Replace("Dto", "");
        int index = result.IndexOf('`');
        if (index != -1)
        {
            result = result.Substring(0, index);
        }
        return result;
    }
}
