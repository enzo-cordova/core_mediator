using System.Security.Claims;

namespace Genzai.Core.Tests.Mock.DbContext;

/// <summary>
/// Context class for testing entity base.
/// </summary>
public class EntityFakeDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityFakeDbContext"/> class.
    /// </summary>
    /// <param name="options">context options.</param>
    public EntityFakeDbContext(DbContextOptions<EntityFakeDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets cars DbSet.
    /// </summary>
    public DbSet<CarTest> Cars { get; set; }

    public DbSet<CarAuditableTest> CarsAuditables { get; set; }

    public DbSet<CarBaseTest> CarsBase { get; set; }

    public int SaveAuditChanges(ClaimsPrincipal User, CancellationToken cancellationToken = default)
    {
        var insertedEntries = this.ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Added)
            .Select(x => x.Entity);

        foreach (var insertedEntry in insertedEntries)
        {
            var type = insertedEntry.GetType();
            var auditableEntity = insertedEntry as IAuditable;
            //If the inserted object is an Auditable.
            if (auditableEntity != null)
            {
                auditableEntity.CreatedInformation(User.Identity?.Name);
            }
        }

        var modifiedEntries = this.ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Modified)
            .Select(x => x.Entity);

        foreach (var modifiedEntry in modifiedEntries)
        {
            //If the inserted object is an Auditable.
            var auditableEntity = modifiedEntry as IAuditable;
            if (auditableEntity != null)
            {
                auditableEntity.UpdateInformation(User.Identity?.Name);
            }
        }

        return SaveChanges();
    }

    /// <summary>
    /// On model creating method.
    /// </summary>
    /// <param name="modelBuilder">Model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarTest>(entity =>
        {
            entity.Property(a => a.Id)
                .IsRequired()
                .IsUnicode(false)
                .ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<CarAuditableTest>(entity =>
        {
            entity.Property(a => a.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            entity.Property(a => a.CreatedAt)
                .IsRequired();

            entity.Property(a => a.CreatedBy)
                .IsRequired(true);
        });

        modelBuilder.Entity<CarBaseTest>(entity =>
        {
            entity.HasKey(x => new { x.Brand, x.Model });

            entity.Property(a => a.Brand)
                .IsRequired();
            entity.Property(a => a.Model)
                .IsRequired();
        });
    }
}