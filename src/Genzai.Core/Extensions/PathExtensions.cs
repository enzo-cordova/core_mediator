namespace Genzai.Core.Extensions;

/// <summary>
/// Path Extensions class.
/// </summary>
public static class PathExtensions
{
    /// <summary>
    /// Get file path from application environment base path.
    /// </summary>
    /// <param name="fileName">File name string.</param>
    /// <returns>Phisical path.</returns>
    public static string GetFilePathFromBasePath(this string fileName)
    {
        return Path.Combine(ApplicationEnvironment.ApplicationBasePath, fileName);
    }
}