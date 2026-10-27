using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Infrastructure.Persistence.Configurations;

public sealed class ParameterMasterConfiguration : IEntityTypeConfiguration<ParameterMaster>
{
    public void Configure(EntityTypeBuilder<ParameterMaster> builder)
    {
        builder.ToTable("tb_sbe_parameter_masters");

        // Primary key
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("parameter_master_id")
            .ValueGeneratedOnAdd();

        // Business properties
        builder.Property(x => x.Key)
            .HasColumnName("key")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.DisplayName)
            .HasColumnName("display_name")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.CategoryId)
            .HasColumnName("category_id")
            .IsRequired();

        builder.Property(x => x.Unit)
            .HasColumnName("unit")
            .HasMaxLength(50)
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

        // Indexes
        builder.HasIndex(x => x.Key);
        builder.HasIndex(x => x.CategoryId);
    }
}
