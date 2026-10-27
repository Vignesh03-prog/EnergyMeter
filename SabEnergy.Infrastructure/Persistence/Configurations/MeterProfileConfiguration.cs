using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Infrastructure.Persistence.Configurations;

public sealed class MeterProfileConfiguration : IEntityTypeConfiguration<MeterProfile>
{
    public void Configure(EntityTypeBuilder<MeterProfile> builder)
    {
        builder.ToTable("tb_sbe_meter_profiles");

        // Primary key
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("meter_profile_id")
            .ValueGeneratedOnAdd();

        // Business properties
        builder.Property(x => x.ProfileVersion)
            .HasColumnName("profile_version")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.MakeId)
            .HasColumnName("make_id")
            .IsRequired();

        builder.Property(x => x.ModelId)
            .HasColumnName("model_id")
            .IsRequired();

        builder.Property(x => x.AddressOffest)
            .HasColumnName("address_offset")
            .IsRequired();

        builder.Property(x => x.HoldingRegister)
            .HasColumnName("holding_register")
            .IsRequired();

        builder.Property(x => x.ByteOrder)
            .HasColumnName("byte_order")
            .IsRequired();

        // Foreign Key Relationships
        builder.HasOne(x => x.MeterMake)
            .WithMany()
            .HasForeignKey(x => x.MakeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.MeterModel)
            .WithMany()
            .HasForeignKey(x => x.ModelId)
            .OnDelete(DeleteBehavior.Restrict);

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

        // Indexes
        builder.HasIndex(x => x.ProfileVersion);
        builder.HasIndex(x => x.MakeId);
        builder.HasIndex(x => x.ModelId);
    }
}
