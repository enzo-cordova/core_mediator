using Genzai.WebCore.Errors;

namespace Genzai.WebCore.Exceptions;

/// <summary>
/// Unauthorized Exception
/// </summary>
[Serializable()]
public class UnauthorizedException : ApplicationErrorException
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="entityName">(User) entity name</param>
    public UnauthorizedException(string entityName)
    {
        ApplicationError error = new ApplicationError("User unauthorized/not logged in.", entityName, "error.user.unauthorized", null);
        Errors = new List<ApplicationError> { error };
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="info">Info</param>
    /// <param name="context">Context</param>
    protected UnauthorizedException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
