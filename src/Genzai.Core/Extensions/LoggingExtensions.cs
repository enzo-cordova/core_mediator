namespace Genzai.Core.Extensions;

/// <summary>
/// Logging extensions enrichment.
/// </summary>
[ExcludeFromCodeCoverage]
public static class LoggingExtensions
{
    /// <summary>
    /// Log debug.
    /// </summary>
    /// <param name="logger">Generic logger.</param>
    /// <param name="message">String message.</param>
    /// <param name="memberName">Caller member name.</param>
    /// <param name="sourceFilePath">Source File path.</param>
    /// <param name="sourceLineNumber">Line Number.</param>
    public static void Debug(
        this ILogger logger,
        string message,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        logger.LogDebug(message.FormatForContext(memberName, sourceFilePath, sourceLineNumber));
    }

    /// <summary>
    /// Exception log debug.
    /// </summary>
    /// <param name="logger">Generic logger.</param>
    /// <param name="message">String message.</param>
    /// <param name="exception">Generic Exception.</param>
    /// <param name="memberName">Caller member name.</param>
    /// <param name="sourceFilePath">Source File path.</param>
    /// <param name="sourceLineNumber">Line Number.</param>
    public static void Debug(
        this ILogger logger,
        string message,
        Exception exception,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        logger.LogDebug(message.FormatForException(exception).FormatForContext(memberName, sourceFilePath, sourceLineNumber));
    }

    /// <summary>
    /// Log Error.
    /// </summary>
    /// <param name="logger">Generic logger.</param>
    /// <param name="message">String message.</param>
    /// <param name="memberName">Caller member name.</param>
    /// <param name="sourceFilePath">Source File path.</param>
    /// <param name="sourceLineNumber">Line Number.</param>
    public static void Error(
        this ILogger logger,
        string message,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        logger.LogError(message.FormatForContext(memberName, sourceFilePath, sourceLineNumber));
    }

    /// <summary>
    /// Exception Log error with message.
    /// </summary>
    /// <param name="logger">Generic logger.</param>
    /// <param name="message">String message.</param>
    /// <param name="exception">Generic Exception.</param>
    /// <param name="memberName">Caller member name.</param>
    /// <param name="sourceFilePath">Source File path.</param>
    /// <param name="sourceLineNumber">Line Number.</param>
    public static void Error(
        this ILogger logger,
        string message,
        Exception exception,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        logger.LogError(message.FormatForException(exception).FormatForContext(memberName, sourceFilePath, sourceLineNumber));
    }

    /// <summary>
    /// Exception Log error.
    /// </summary>
    /// <param name="logger">Generic logger.</param>
    /// <param name="exception">Generic Exception.</param>
    /// <param name="memberName">Caller member name.</param>
    /// <param name="sourceFilePath">Source File path.</param>
    /// <param name="sourceLineNumber">Line Number.</param>
    public static void Error(
        this ILogger logger,
        Exception exception,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        logger.LogError((exception != null ? exception.ToString() : string.Empty).FormatForContext(memberName, sourceFilePath, sourceLineNumber));
    }

    /// <summary>
    /// Log information.
    /// </summary>
    /// <param name="logger">Generic logger.</param>
    /// <param name="message">String message.</param>
    /// <param name="memberName">Caller member name.</param>
    /// <param name="sourceFilePath">Source File path.</param>
    public static void Info(
        this ILogger logger,
        string message,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "")
    {
        logger.LogInformation(message.FormatForInfo(memberName, sourceFilePath));
    }

    /// <summary>
    /// Exception log information.
    /// </summary>
    /// <param name="logger">Generic logger.</param>
    /// <param name="message">String message.</param>
    /// <param name="exception">Generic Exception.</param>
    /// <param name="memberName">Caller member name.</param>
    /// <param name="sourceFilePath">Source File path.</param>
    /// <param name="sourceLineNumber">Line Number.</param>
    public static void Info(
        this ILogger logger,
        string message,
        Exception exception,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        logger.LogInformation(message.FormatForException(exception).FormatForContext(memberName, sourceFilePath, sourceLineNumber));
    }

    /// <summary>
    /// Log Warn.
    /// </summary>
    /// <param name="logger">Generic logger.</param>
    /// <param name="message">String message.</param>
    /// <param name="memberName">Caller member name.</param>
    /// <param name="sourceFilePath">Source File path.</param>
    /// <param name="sourceLineNumber">Line Number.</param>
    public static void Warn(
        this ILogger logger,
        string message,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        logger.LogWarning(message.FormatForContext(memberName, sourceFilePath, sourceLineNumber));
    }

    /// <summary>
    /// Exception Log Warn.
    /// </summary>
    /// <param name="logger">Generic logger.</param>
    /// <param name="message">String message.</param>
    /// <param name="exception">Generic Exception.</param>
    /// <param name="memberName">Caller member name.</param>
    /// <param name="sourceFilePath">Source File path.</param>
    /// <param name="sourceLineNumber">Line Number.</param>
    public static void Warn(
        this ILogger logger,
        string message,
        Exception exception,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        logger.LogWarning(message.FormatForException(exception).FormatForContext(memberName, sourceFilePath, sourceLineNumber));
    }

    /// <summary>
    /// Format for info.
    /// </summary>
    /// <param name="message">String message.</param>
    /// <param name="memberName">Caller member name.</param>
    /// <param name="sourceFilePath">Source File path.</param>
    /// <returns>String message result.</returns>
    private static string FormatForInfo(
        this string message,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "")
    {
        var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);

        return $"[{fileName}] [{memberName}] {message}";
    }

    /// <summary>
    /// Format for context.
    /// </summary>
    /// <param name="message">String message.</param>
    /// <param name="memberName">Caller member name.</param>
    /// <param name="sourceFilePath">Source File path.</param>
    /// <param name="sourceLineNumber">Line Number.</param>
    /// <returns>String message result.</returns>
    private static string FormatForContext(
        this string message,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);
        var methodName = memberName;

        return $"[{fileName}] [{methodName}] [{sourceLineNumber}] {message}";
    }

    /// <summary>
    /// Format for exception.
    /// </summary>
    /// <param name="message">String message.</param>
    /// <param name="exception">Generic Exception.</param>
    /// <returns>String message result.</returns>
    private static string FormatForException(this string message, Exception exception)
    {
        return $"{message}: {(exception != null ? exception.ToString() : string.Empty)}";
    }
}