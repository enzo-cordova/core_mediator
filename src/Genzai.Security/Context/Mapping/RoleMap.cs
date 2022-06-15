using Genzai.Security.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genzai.Security.Context.Mapping;

/// <summary>
/// Mapping for Role Table
/// </summary>
public class RoleMap : IEntityTypeConfiguration<Role>
{

    /// <summary>
    /// Database configuration for role table
    /// </summary>
    /// <param name="builder">Builder</param>
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("role");
        builder.HasKey(x => x.Id);
        builder
            .Property(p => p.Id).ValueGeneratedOnAdd()
            .HasColumnName("id");

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Code)
            .HasColumnName("code")
            .HasMaxLength(10)
            .IsRequired();

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

        //Se borran los HasData por las fechas de creación, y es un método privado y no 
        //se puede inicializar en la construcción por tanto, no se puede utilizar
    }

}