using Domain.Conditioners;
using Domain.ConditionerStatuses;
using Domain.ConditionerTypes;
using Domain.Manufacturers;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ConditionerConfiguration : IEntityTypeConfiguration<Conditioner>
{
    public void Configure(EntityTypeBuilder<Conditioner> builder)
    {
        // 🔹 Первинний ключ
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(id => id.Value, value => new ConditionerId(value))
            .ValueGeneratedNever();

        // 🔹 Основні властивості
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

        // 🔹 Зв’язки через value object-и
        builder.Property(x => x.StatusId)
            .HasConversion(id => id.Value, value => new ConditionerStatusId(value))
            .IsRequired();

        builder.Property(x => x.TypeId)
            .HasConversion(id => id.Value, value => new ConditionerTypeId(value))
            .IsRequired();

        builder.Property(x => x.ManufacturerId)
            .HasConversion(id => id.Value, value => new ManufacturerId(value))
            .IsRequired();

        // 🔹 Навігаційні властивості та зв’язки (1:N)
        builder.HasOne(x => x.Status)
            .WithMany()
            .HasForeignKey(x => x.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Type)
            .WithMany()
            .HasForeignKey(x => x.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Manufacturer)
            .WithMany()
            .HasForeignKey(x => x.ManufacturerId)
            .OnDelete(DeleteBehavior.Restrict);

        // 🔹 Аудитні поля
        builder.Property(x => x.CreatedAt)
            .HasConversion(new DateTimeUtcConverter())
            .IsRequired()
            .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

        builder.Property(x => x.UpdatedAt)
            .HasConversion(new DateTimeUtcConverter())
            .IsRequired(false);
    }
}
