using System.Reflection;

namespace Genzai.WebCore.Utils;

/// <summary>
/// Clases for utils in attributes
/// </summary>
public static class AttributeUtils
{

    /// <summary>
    /// Indicates if method have the attribute request
    /// </summary>
    /// <typeparam name="TResult">Attribute to check</typeparam>
    /// <param name="methodInfo">Method information</param>
    /// <returns>True if have the attribute, false in other case</returns>
    public static bool HaveAttribute<TResult>(MethodInfo methodInfo)
    {
        IEnumerable<TResult> attribute = methodInfo!.DeclaringType!.GetCustomAttributes(true)
            .Union(methodInfo.GetCustomAttributes(true))
            .OfType<TResult>();
        return attribute.Any();
    }


}
