using Microsoft.Extensions.Logging;

namespace Genzai.WebCore.Logging;

/// <summary>
/// Logger Adapter
/// </summary>
/// <typeparam name="T">Log type</typeparam>
public class LoggerAdapter<T> : ILoggerAdapter<T>
{
    private readonly ILogger<T> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    public LoggerAdapter(ILogger<T> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Log debug message
    /// </summary>
    /// <param name="message">Message</param>
    /// <param name="args">Arguments</param>
    public void LogDebug(string message, params object[] args)
    {
        _logger.LogDebug(message, args);
    }

    /// <summary>
    /// Log error message
    /// </summary>
    /// <param name="ex">Exception</param>
    /// <param name="message">Message</param>
    /// <param name="args">Arguments</param>
    public void LogError(Exception ex, string message, params object[] args)
    {
        _logger.LogError(ex, message, args);
    }

    /// <summary>
    /// Log warning message
    /// </summary>
    /// <param name="message">Message</param>
    /// <param name="args">Arguments</param>
    public void LogInformation(string message, params object[] args)
    {
        _logger.LogInformation(message, args);
    }
}
