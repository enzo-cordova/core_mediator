namespace Genzai.Core.Tests.Mock.Entities;

/// <summary>
/// Manager response.
/// </summary>
public class ManagerResponse : ModelResponse<ManagerTest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ManagerResponse"/> class.
    /// </summary>
    /// <param name="item">Manager test.</param>
    public ManagerResponse(ManagerTest item) : base(item)
    {
    }
}