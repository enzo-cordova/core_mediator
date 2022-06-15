using Genzai.WebCore.Commands.Delete;

namespace Genzai.WebCore.Test.Mock.Application.Commands.Delete
{
    /// <summary>
    /// Command for delete sample handler
    /// </summary>
    public class DeleteSampleCommand : BaseDeleteCommand
    {
        public DeleteSampleCommand(long id) : base(id)
        {
        }
    }
}