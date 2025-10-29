using System.Reflection;
using Domain.Conditioner;
using Domain.ConditionerStatus;
using Domain.ConditionerType;
using Domain.Entities;
using Domain.MaintenanceSchedules;
using Domain.Manufacturer;
using Domain.WorkOrder;
using Domain.WorkOrderPriorities;
using Domain.WorkOrderStatuses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Conditioner> Conditioners { get; init; }
    public DbSet<ConditionerStatus> ConditionerStatuses { get; init; }
    public DbSet<ConditionerType> ConditionerTypes { get; init; }
    public DbSet<Manufacturer> Manufacturers { get; init; }
    public DbSet<MaintenanceFrequency> MaintenanceFrequencies { get; init; }
    public DbSet<MaintenanceSchedule> MaintenanceSchedules { get; init; }
    public DbSet<WorkOrder> WorkOrders { get; init; }
    public DbSet<WorkOrderPriority> WorkOrderPriorities { get; init; }
    public DbSet<WorkOrderStatus> WorkOrderStatuses { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
