using Domain.Conditioners;
using Domain.MaintenanceSchedules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ConditionerMaintenanceScheduleConfiguration : IEntityTypeConfiguration<ConditionerMaintenanceSchedule>
{
    public void Configure(EntityTypeBuilder<ConditionerMaintenanceSchedule> builder)
    {
        // Складений ключ
        builder.HasKey(x => new { x.ConditionerId, x.MaintenanceScheduleId });

        // Конверсія Value Object → Guid і назад
        builder.Property(x => x.ConditionerId)
            .HasConversion(id => id.Value, value => new ConditionerId(value));

        builder.Property(x => x.MaintenanceScheduleId)
            .HasConversion(id => id.Value, value => new MaintenanceScheduleId(value));

        // Відношення Conditioner ↔ ConditionerMaintenanceSchedule
        builder.HasOne(x => x.Conditioner)
            .WithMany(x => x.MaintenanceSchedules)
            .HasForeignKey(x => x.ConditionerId)
            .HasConstraintName("fk_conditioner_maintenance_schedule_conditioners_id")
            .OnDelete(DeleteBehavior.Cascade);

        // Відношення MaintenanceSchedule ↔ ConditionerMaintenanceSchedule
        builder.HasOne(x => x.MaintenanceSchedule)
            .WithMany(x => x.Conditioners)
            .HasForeignKey(x => x.MaintenanceScheduleId)
            .HasConstraintName("fk_conditioner_maintenance_schedule_schedules_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
