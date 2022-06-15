namespace Genzai.Core.Extensions;

/// <summary>
/// Enum extensions class.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Get enum member attribute value.
    /// </summary>
    /// <param name="value">Enum type.</param>
    /// <returns>string value.</returns>
    public static string GetEnumMemberAttributeValue(this Enum value)
    {
        if (value == null)
        {
            return string.Empty;
        }
        else
        {
            var attributes = value
                .GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(EnumMemberAttribute), false) as EnumMemberAttribute[];

            return attributes.Length > 0 ? attributes[0].Value : string.Empty;
        }
    }
}