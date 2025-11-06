using System.Data;
using System.Reflection;
using Application.Common.Interfaces;
using Domain.Conditioners;
using Domain.ConditionerStatuses;
using Domain.ConditionerTypes;
using Domain.MaintenanceFrequencies;
using Domain.MaintenanceSchedules;
using Domain.Manufacturers;
using Domain.WorkOrders;
using Domain.WorkOrderPriorities;
using Domain.WorkOrderStatuses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
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

    public async Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        var transaction = await Database.BeginTransactionAsync(cancellationToken);
        return transaction.GetDbTransaction();
    }
}
