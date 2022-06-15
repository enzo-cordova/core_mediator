using Genzai.WebCore.Test.Mock.Domain.Persistence.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genzai.WebCore.Test.Mock.Infrastructure.Persistence.Mapping;

/// <summary>
/// Entity configuration for sample
/// </summary>
public class SampleEntityConfiguration : IEntityTypeConfiguration<Sample>
{
    /// <summary>
    /// Database configuration for sample
    /// </summary>
    /// <param name="builder">Builder</param>
    public void Configure(EntityTypeBuilder<Sample> builder)
    {
        builder.ToTable("sample");
        builder.HasKey(x => x.Id);
        builder
        .Property(p => p.Id).ValueGeneratedOnAdd()
        .HasColumnName("id");

        builder.Property(x => x.Name)
        .HasColumnName("name")
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(x => x.SubSampleId)
       .HasColumnName("subsample_id")
       .HasMaxLength(10)
       .IsRequired();

    }
}
