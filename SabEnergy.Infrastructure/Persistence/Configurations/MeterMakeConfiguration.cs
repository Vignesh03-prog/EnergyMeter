using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Infrastructure.Persistence.Configurations;

public sealed class MeterMakeConfiguration : IEntityTypeConfiguration<MeterMake>
{
    public void Configure(EntityTypeBuilder<MeterMake> builder)
    {
        builder.ToTable("tb_sbe_meter_makes");

        // Primary key
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("metermake_id")
            .ValueGeneratedOnAdd();

        // Business property
        builder.Property(x => x.MeterMakeName)
            .HasColumnName("metermake_name")
            .HasMaxLength(150)
            .IsRequired();

        // Audit & BaseEntity properties
        builder.Property(x => x.CreatedBy)
            .HasColumnName("created_by")
            .IsRequired();

        builder.Property(x => x.UpdatedBy)
            .HasColumnName("updated_by")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.IsDeleted)
            .HasColumnName("is_deleted")
            .IsRequired()
            .HasDefaultValue(false);

        // Index
        builder.HasIndex(x => x.MeterMakeName);
    }
}
