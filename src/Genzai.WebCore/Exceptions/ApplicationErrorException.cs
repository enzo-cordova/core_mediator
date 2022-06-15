using Genzai.WebCore.Errors;

namespace Genzai.WebCore.Exceptions;

/// <summary>
/// Base Exception
/// </summary>
[Serializable()]
public class ApplicationErrorException : Exception
{

    /// <summary>
    /// Errors
    /// </summary>
    public IList<ApplicationError> Errors { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public ApplicationErrorException()
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="errors">Errors</param>
    public ApplicationErrorException(IList<ApplicationError> errors)
    {
        this.Errors = errors;
    }


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="error">Error</param>
    public ApplicationErrorException(ApplicationError error)
    {
        this.Errors = new List<ApplicationError>
            {
                error
            };
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="info">Info</param>
    /// <param name="context">Context</param>
    protected ApplicationErrorException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
