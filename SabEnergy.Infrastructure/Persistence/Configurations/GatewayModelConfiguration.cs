using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SabEnergy.Domain.Modules.Masters.Model;

namespace SabEnergy.Infrastructure.Persistence.Configurations;

public sealed class GatewayModelConfiguration : IEntityTypeConfiguration<GatewayModel>
{
    public void Configure(EntityTypeBuilder<GatewayModel> builder)
    {
        builder.ToTable("tb_sbe_gateway_models");

        // Primary key
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("gateway_model_id")
            .ValueGeneratedOnAdd();

        // Business property
        builder.Property(x => x.GatewayModelName)
            .HasColumnName("gateway_model_name")
            .HasMaxLength(150)
            .IsRequired();

        // Foreign key
        builder.Property(x => x.GatewayMakeId)
            .HasColumnName("gateway_make_id")
            .IsRequired();

        builder.HasOne(x => x.GatewayMake)
            .WithMany()
            .HasForeignKey(x => x.GatewayMakeId)
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
        builder.HasIndex(x => x.GatewayModelName);
        builder.HasIndex(x => x.GatewayMakeId);
    }
}
