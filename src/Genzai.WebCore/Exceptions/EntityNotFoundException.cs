using Genzai.WebCore.Errors;

namespace Genzai.WebCore.Exceptions;

/// <summary>
/// Enity not found exception
/// </summary>
[Serializable()]
public class EntityNotFoundException : ApplicationErrorException
{
    /// <summary>
    /// Constructory
    /// </summary>
    /// <param name="type">Entity type</param>
    public EntityNotFoundException(string type)
    {
        ApplicationError error = new ApplicationError($"Entity of type {type} not found.", type, $"error.{type}.notfound", null);
        this.Errors = new List<ApplicationError> { error };
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="info">Info</param>
    /// <param name="context">Context</param>
    protected EntityNotFoundException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
