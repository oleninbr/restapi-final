using Domain.Entities;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ConditionerConfiguration : IEntityTypeConfiguration<Conditioner>
{
    public void Configure(EntityTypeBuilder<Conditioner> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Model)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.SerialNumber)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Location)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.InstallationDate)
            .HasConversion(new DateTimeUtcConverter())
            .IsRequired();

        builder.Property(x => x.StatusId)
            .IsRequired();

        builder.Property(x => x.TypeId)
            .IsRequired();

        builder.Property(x => x.ManufacturerId)
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
