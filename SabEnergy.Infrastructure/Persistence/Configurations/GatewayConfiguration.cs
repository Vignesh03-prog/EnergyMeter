using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Infrastructure.Persistence.Configurations;

public sealed class GatewayConfiguration : IEntityTypeConfiguration<Gateway>
{
    public void Configure(EntityTypeBuilder<Gateway> builder)
    {
        builder.ToTable("tb_sbe_gateways");

        // Primary key
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("gateway_id")
            .ValueGeneratedOnAdd();

        // Business properties
        builder.Property(x => x.DeviceName)
            .HasColumnName("device_name")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.SerialNo)
            .HasColumnName("serial_no")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.GatewayMakeId)
            .HasColumnName("gateway_make_id")
            .IsRequired();

        builder.Property(x => x.GatewayModelId)
            .HasColumnName("gateway_model_id")
            .IsRequired(false);

        builder.Property(x => x.IpAddress)
            .HasColumnName("ip_address")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.MacAddress)
            .HasColumnName("mac_address")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.ChannelCount)
            .HasColumnName("channel_count")
            .IsRequired();

        builder.Property(x => x.FirmwareVersion)
            .HasColumnName("firmware_version")
            .HasMaxLength(50);

        builder.Property(x => x.KeepaliveTime)
            .HasColumnName("keepalive_time")
            .IsRequired();

        builder.Property(x => x.Wifi_SSID)
            .HasColumnName("wifi_ssid")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Wifi_Port)
            .HasColumnName("wifi_port");

        // Relationships (Foreign Keys)
        builder.HasOne(x => x.GatewayMake)
            .WithMany()
            .HasForeignKey(x => x.GatewayMakeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.GatewayModel)
            .WithMany()
            .HasForeignKey(x => x.GatewayModelId)
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
        builder.HasIndex(x => x.DeviceName);
        builder.HasIndex(x => x.SerialNo);
        builder.HasIndex(x => x.GatewayMakeId);
        builder.HasIndex(x => x.GatewayModelId);
    }
}
