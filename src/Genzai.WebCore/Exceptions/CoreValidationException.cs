using Genzai.WebCore.Errors;

namespace Genzai.WebCore.Exceptions;

/// <summary>
/// Validation exception
/// </summary>
[Serializable()]
public class CoreValidationException : ApplicationErrorException
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="errors">Errors</param>
    public CoreValidationException(IList<ApplicationError> errors) : base(errors)
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="error">Error</param>
    public CoreValidationException(ApplicationError error) : base(error)
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="info">Info</param>
    /// <param name="context">Context</param>
    protected CoreValidationException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
