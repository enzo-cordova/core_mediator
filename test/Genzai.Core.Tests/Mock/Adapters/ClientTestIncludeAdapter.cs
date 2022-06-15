namespace Genzai.Core.Tests.Mock.Adapters;

/// <summary>
/// Client include adapter for testing.
/// </summary>
public class ClientTestIncludeAdapter : IncludesAdapter<ClientTest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ClientTestIncludeAdapter"/> class.
    /// </summary>
    /// <param name="expression"></param>
    public ClientTestIncludeAdapter(Func<IQueryable<ClientTest>, IQueryable<ClientTest>> expression)
        : base(expression)
    {
    }
}