using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Infrastructure.Persistence.Configurations;

public sealed class MeterConfiguration : IEntityTypeConfiguration<Meter>
{
    public void Configure(EntityTypeBuilder<Meter> builder)
    {
        builder.ToTable("tb_sbe_meters");

        // Primary key
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("meter_id")
            .ValueGeneratedOnAdd();

        // Business properties
        builder.Property(x => x.SerialNo)
            .HasColumnName("serial_no")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.CustomName)
            .HasColumnName("custom_name")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.ChannelId)
            .HasColumnName("channel_id")
            .IsRequired();

        builder.Property(x => x.SlaveId)
            .HasColumnName("slave_id")
            .IsRequired();

        builder.Property(x => x.DeviceProfileId)
            .HasColumnName("device_profile_id")
            .IsRequired();

        builder.Property(x => x.MeterMakeId)
            .HasColumnName("meter_make_id")
            .IsRequired();

        builder.Property(x => x.MeterModelId)
            .HasColumnName("meter_model_id")
            .IsRequired(false);

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.BaudRate)
            .HasColumnName("baud_rate")
            .IsRequired();

        builder.Property(x => x.ParityBit)
            .HasColumnName("parity_bit")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.StopBit)
            .HasColumnName("stop_bit")
            .IsRequired();

        builder.Property(x => x.DataBit)
            .HasColumnName("data_bit")
            .IsRequired();

        // Foreign Key Relationships
        builder.HasOne(x => x.MeterMake)
            .WithMany()
            .HasForeignKey(x => x.MeterMakeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.MeterModel)
            .WithMany()
            .HasForeignKey(x => x.MeterModelId)
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
        builder.HasIndex(x => x.SerialNo);
        builder.HasIndex(x => x.CustomName);
        builder.HasIndex(x => x.MeterMakeId);
        builder.HasIndex(x => x.MeterModelId);
    }
}
