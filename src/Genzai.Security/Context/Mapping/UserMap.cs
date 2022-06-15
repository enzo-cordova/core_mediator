using Genzai.Security.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static Genzai.Security.Domain.User;

namespace Genzai.Security.Context.Mapping;

/// <summary>
/// Mapping for users
/// </summary>
public class UserMap : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Configure method.
    /// </summary>
    /// <param name="builder">Entity builder.</param>
    [ExcludeFromCodeCoverage]
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");

        builder.HasIndex(e => e.RoleId, "IX_user_RoleId");

        builder.HasKey(x => x.Id);
        builder
            .Property(p => p.Id).ValueGeneratedOnAdd()
            .HasColumnName("id");

        builder.Property(e => e.Active)
            .IsRequired()
            .HasColumnName("active")
            .HasDefaultValueSql("'1'");

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(256)
            .HasColumnName("code");

        builder.Property(e => e.Email)
            .HasMaxLength(255)
            .HasColumnName("email");

        builder.Property(e => e.FamilyName)
            .HasMaxLength(255)
            .HasColumnName("familyname");

        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");

        builder.Property(s => s.TypeAuthenticated)
          .HasColumnType("enum('0','1','2')")
            .HasColumnName("type_authenticated")
            .HasDefaultValue(Authenticated.Public)
            .HasConversion<int>();

        builder.HasOne(d => d.Role)
            .WithMany(p => p.Users)
            .HasForeignKey(d => d.RoleId);

        builder.Property(x => x.CreatedBy)
            .HasColumnName("created_by")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.UpdatedBy)
            .HasColumnName("updated_by")
            .HasMaxLength(255);

        builder.Property(c => c.UpdatedAt)
            .HasColumnName("modified_date").IsRequired(false).
            HasConversion(v => v, v => DateTime.SpecifyKind((DateTime)v, DateTimeKind.Utc));


        builder.Property(c => c.CreatedAt)
            .HasColumnName("creation_date").IsRequired()
            .HasDefaultValue(DateTime.UtcNow)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}