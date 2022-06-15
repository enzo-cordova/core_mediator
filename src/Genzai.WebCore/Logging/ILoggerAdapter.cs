namespace Genzai.WebCore.Logging;

/// <summary>
/// Logger Adapter interface
/// </summary>
/// <typeparam name="T">Log type</typeparam>
public interface ILoggerAdapter<in T>
{

    /// <summary>
    /// Log debug message
    /// </summary>
    /// <param name="message">Message</param>
    /// <param name="args">Arguments</param>
    void LogDebug(string message, params object[] args);

    /// <summary>
    /// Log information message
    /// </summary>
    /// <param name="message">Message</param>
    /// <param name="args">Arguments</param>
    void LogInformation(string message, params object[] args);

    /// <summary>
    /// Log error message
    /// </summary>
    /// <param name="ex">Exception</param>
    /// <param name="message">Message</param>
    /// <param name="args">Arguments</param>
    void LogError(Exception ex, string message, params object[] args);
}
