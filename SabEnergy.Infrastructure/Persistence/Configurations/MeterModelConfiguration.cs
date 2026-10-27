using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SabEnergy.Domain.Modules.Masters.Model;

namespace SabEnergy.Infrastructure.Persistence.Configurations;

public sealed class MeterModelConfiguration : IEntityTypeConfiguration<MeterModel>
{
    public void Configure(EntityTypeBuilder<MeterModel> builder)
    {
        builder.ToTable("tb_sbe_meter_models");

        // Primary key
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("meter_model_id")
            .ValueGeneratedOnAdd();

        // Business property
        builder.Property(x => x.MeterModelName)
            .HasColumnName("meter_model_name")
            .HasMaxLength(150)
            .IsRequired();

        // Foreign key
        builder.Property(x => x.MeterMakeId)
            .HasColumnName("meter_make_id")
            .IsRequired();

        builder.HasOne(x => x.MeterMake)
            .WithMany()
            .HasForeignKey(x => x.MeterMakeId)
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
        builder.HasIndex(x => x.MeterModelName);
        builder.HasIndex(x => x.MeterMakeId);
    }
}
