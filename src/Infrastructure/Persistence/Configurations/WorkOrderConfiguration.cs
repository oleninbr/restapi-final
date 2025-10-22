using Domain.Entities;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
{
    public void Configure(EntityTypeBuilder<WorkOrder> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.WorkOrderNumber)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.ScheduledDate)
            .HasConversion(new DateTimeUtcConverter())
            .IsRequired();

        builder.Property(x => x.ConditionerId)
            .IsRequired();

        builder.Property(x => x.PriorityId)
            .IsRequired();

        builder.Property(x => x.StatusId)
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
