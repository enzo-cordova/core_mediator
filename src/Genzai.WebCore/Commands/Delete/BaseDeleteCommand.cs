using MediatR;

namespace Genzai.WebCore.Commands.Delete;

/// <summary>
/// Base delete command
/// </summary>
public abstract class BaseDeleteCommand : IRequest<bool>
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="id">Id</param>
    protected BaseDeleteCommand(long id)
    {
        Id = id;
    }
}
