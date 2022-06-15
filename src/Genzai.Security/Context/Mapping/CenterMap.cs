using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Genzai.EfCore.Map;
using Genzai.Security.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genzai.Security.Context.Mapping;

/// <summary>
/// Mapping for center
/// </summary>
[ExcludeFromCodeCoverage]

public class CenterMap : IEntityTypeConfiguration<Center>
{
    /// <summary>
    /// Database configuration for center
    /// </summary>
    /// <param name="builder">Builder</param>
    public void Configure(EntityTypeBuilder<Center> builder)
    {
        builder.ToTable("center");
        builder.HasKey(sc => sc.Id);
        builder
            .Property(sc => sc.Id).ValueGeneratedOnAdd()
            .HasColumnName("id");

        builder.Property(sc => sc.Name)
            .HasColumnName("name")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(sc => sc.CenterCode)
            .HasColumnName("center_code")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(sc => sc.CrmId)
            .HasColumnName("crm_id")
            .HasMaxLength(255);

        builder.Property(sc => sc.CustomerId)
            .HasColumnName("customer_id")
            .IsRequired();


        builder.Property(c => c.AddressStreetType)
            .HasColumnName("address_streettype")
            .HasMaxLength(1024);

        builder.Property(c => c.AddresStreettypeCode)
            .HasColumnName("address_streettypecode")
            .HasMaxLength(255);

        builder.Property(c => c.AddressProvinceName)
            .HasColumnName("address_province_name")
            .HasMaxLength(255);

        builder.Property(c => c.AddressFloor)
            .HasColumnName("address_floor")
            .HasMaxLength(255);

        builder.Property(c => c.AddressNumber)
            .HasColumnName("address_number")
            .HasMaxLength(255);

        builder.Property(c => c.AddressStreetName)
            .HasColumnName("address_streetname")
            .HasMaxLength(1024);

        builder.Property(c => c.AdrressPostCode)
            .HasColumnName("address_postcode")
            .HasMaxLength(255);

        builder.Property(c => c.AddressPopulationName)
            .HasColumnName("address_population_name")
            .HasMaxLength(255);

        builder.Property(c => c.CreatedBy)
            .HasColumnName("created_by").IsRequired(false)
            .HasMaxLength(255);

        builder.Property(c => c.UpdatedBy)
            .HasColumnName("updated_by")
            .HasMaxLength(255);

        

        builder.Property(c => c.Description)
            .HasColumnName("description")
            .HasMaxLength(1024);

        builder.Property(c => c.Active)
            .HasColumnName("active").IsRequired();

        builder.Property(c => c.Assigned)
            .HasColumnName("assigned")
            .IsRequired()
            .HasDefaultValue(false);

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




    }
}