using Domain.Entities;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class WorkOrderStatusConfiguration : IEntityTypeConfiguration<WorkOrderStatus>
{
    public void Configure(EntityTypeBuilder<WorkOrderStatus> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasConversion(new DateTimeUtcConverter())
            .IsRequired()
            .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

        builder.Property(x => x.UpdatedAt)
            .HasConversion(new DateTimeUtcConverter())
            .IsRequired(false);
    }
}