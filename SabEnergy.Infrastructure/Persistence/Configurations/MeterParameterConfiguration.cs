using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Infrastructure.Persistence.Configurations;

public sealed class MeterParameterConfiguration : IEntityTypeConfiguration<MeterParameter>
{
    public void Configure(EntityTypeBuilder<MeterParameter> builder)
    {
        builder.ToTable("tb_sbe_meter_parameters");

        // Primary key
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("meter_parameter_id")
            .ValueGeneratedOnAdd();

        // Foreign keys
        builder.Property(x => x.MeterProfileId)
            .HasColumnName("meter_profile_id")
            .IsRequired();

        builder.Property(x => x.ParameterMasterId)
            .HasColumnName("parameter_master_id")
            .IsRequired();

        // Business properties
        builder.Property(x => x.Address)
            .HasColumnName("address")
            .IsRequired();

        builder.Property(x => x.DataType)
            .HasColumnName("data_type")
            .IsRequired();

        builder.Property(x => x.Multiplier)
            .HasColumnName("multiplier")
            .IsRequired();

        // Foreign Key Relationships
        builder.HasOne(x => x.MeterProfile)
            .WithMany()
            .HasForeignKey(x => x.MeterProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ParameterMaster)
            .WithMany()
            .HasForeignKey(x => x.ParameterMasterId)
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
        builder.HasIndex(x => x.MeterProfileId);
        builder.HasIndex(x => x.ParameterMasterId);
    }
}
