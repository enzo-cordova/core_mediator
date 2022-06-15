using Genzai.Security.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace Genzai.Security.Context.Mapping;

/// <summary>
/// Mapping for permissions
/// </summary>
public class PermissionMap : IEntityTypeConfiguration<Permission>
{
    /// <summary>
    /// Database configuration for Permission
    /// </summary>
    /// <param name="builder">Builder</param>
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permission");
        builder.HasKey(x => x.Id);
        builder
            .Property(p => p.Id)
            .HasColumnName("id");

        builder
            .Property(p => p.Name)
            .HasColumnName("name");

        builder
            .Property(p => p.Value)
            .HasColumnName("value");

        builder.Property(x => x.CreatedBy)
            .HasColumnName("created_by")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.UpdatedBy)
            .HasColumnName("updated_by")
            .HasMaxLength(255);

        builder.Property(c => c.UpdatedAt)
            .HasColumnName("modified_date")
            .IsRequired(false)
            .HasColumnType("DATETIME")
            .HasConversion(v => v, v => DateTime.SpecifyKind((DateTime)v, DateTimeKind.Utc));


        builder.Property(c => c.CreatedAt)
            .HasColumnName("creation_date").IsRequired()
            .HasColumnType("DATETIME")
            .HasDefaultValueSql("(CURRENT_TIMESTAMP)")
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));


        builder
            .HasMany(d => d.Roles)
            .WithMany(p => p.Permissions)
            .UsingEntity<Dictionary<string, object>>(
                "Permissionrole",
                l => l.HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RolesId")
                    .HasConstraintName("FK_PermissionRole_role_RolesId"),
                r => r.HasOne<Permission>()
                    .WithMany()
                    .HasForeignKey("PermissionsId")
                    .HasConstraintName("FK_PermissionRole_permission_PermissionsId"),
                j =>
                {
                    j.HasKey("PermissionsId", "RolesId")
                        .HasName("PK_PermissionRole")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    j.ToTable("permissionrole");
                    j.HasIndex(new[] { "RolesId" }, "IX_PermissionRole_RolesId");
                }
            ); 
    }
}