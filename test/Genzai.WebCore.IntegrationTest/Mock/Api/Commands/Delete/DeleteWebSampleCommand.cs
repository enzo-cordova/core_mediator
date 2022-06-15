using Genzai.WebCore.Commands.Delete;

namespace Genzai.WebCore.Integration.Test.Mock.Api.Commands.Delete
{
    /// <summary>
    /// Command for delete sample handler
    /// </summary>
    public class DeleteWebSampleCommand : BaseDeleteCommand
    {
        public DeleteWebSampleCommand(long id) : base(id)
        {
        }
    }
}