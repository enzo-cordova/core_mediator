namespace Genzai.Core.Tests.Mock.DbContext;

/// <summary>
/// Fake DbContext for testing purposes.
/// </summary>
public class FakeDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FakeDbContext"/> class.
    /// </summary>
    /// <param name="options">context options.</param>
    public FakeDbContext(DbContextOptions<FakeDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or Set Clients DbSet.
    /// </summary>
    public DbSet<ClientTest> Clients { get; set; }

    /// <summary>
    /// Gets or set Managers DbSet.
    /// </summary>
    public DbSet<ManagerTest> Managers { get; set; }
}