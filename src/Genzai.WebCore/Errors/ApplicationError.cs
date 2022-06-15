namespace Genzai.WebCore.Errors;

/// <summary>
/// Dto for errors
/// </summary>
public class ApplicationError
{
    private const string Error = "error.";

    /// <summary>
    /// Construcot
    /// </summary>
    /// <param name="message">Validation error message</param>
    /// <param name="type">Entity type</param>
    /// <param name="code">Validation error code</param>
    /// <param name="parameters">Error code parameters</param>
    public ApplicationError(string message, string type, string code, IDictionary<string, string> parameters)
    {
        string realCode = code;
        if (!string.IsNullOrEmpty(realCode) && !string.IsNullOrEmpty(Type) && !realCode.StartsWith(Error))
        {
            realCode = Error + type.ToLower() + "." + realCode;
        }
        Message = message;
        Type = type;
        Code = realCode;
        Parameters = parameters;
    }

    /// <summary>
    /// Type of error
    /// </summary>
    public string Type { get; set; }
    /// <summary>
    /// Error code
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Parameters of error
    /// </summary>
    public IDictionary<string, string> Parameters { get; set; }

    /// <summary>
    /// Message of exception
    /// </summary>
    public string Message { get; set; }

}
