using Application.Common.Interfaces;
using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Infrastructure.Emails;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;


namespace Infrastructure.Persistence;

public static class ConfigurePersistenceServicesExtensions
{

    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("DefaultConnection"));
        dataSourceBuilder.EnableDynamicJson();
        var dataSource = dataSourceBuilder.Build();

        services.AddDbContext<ApplicationDbContext>(options => options
            .UseNpgsql(
                dataSource,
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            //.UseSnakeCaseNamingConvention()
            .ConfigureWarnings(w => w.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning)));

        //added for interface mapping
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        //added email service
        services.AddScoped<IEmailSendingService, EmailSendingService>();

        services.AddScoped<ApplicationDbContextInitialiser>();
        services.AddRepositories();
    }
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ConditionerRepository>();
        services.AddScoped<IConditionerRepository>(provider => provider.GetRequiredService<ConditionerRepository>());
        services.AddScoped<IConditionerQueries>(provider => provider.GetRequiredService<ConditionerRepository>());

        services.AddScoped<ManufacturerRepository>();
        services.AddScoped<IManufacturerRepository>(provider => provider.GetRequiredService<ManufacturerRepository>());
        services.AddScoped<IManufacturerQueries>(provider => provider.GetRequiredService<ManufacturerRepository>());

        services.AddScoped<ConditionerStatusRepository>();
        services.AddScoped<IConditionerStatusRepository>(provider => provider.GetRequiredService<ConditionerStatusRepository>());
        services.AddScoped<IConditionerStatusQueries>(provider => provider.GetRequiredService<ConditionerStatusRepository>());

        services.AddScoped<ConditionerTypeRepository>();
        services.AddScoped<IConditionerTypeRepository>(provider => provider.GetRequiredService<ConditionerTypeRepository>());
        services.AddScoped<IConditionerTypeQueries>(provider => provider.GetRequiredService<ConditionerTypeRepository>());

        services.AddScoped<MaintenanceFrequencyRepository>();
        services.AddScoped<IMaintenanceFrequencyRepository>(provider => provider.GetRequiredService<MaintenanceFrequencyRepository>());
        services.AddScoped<IMaintenanceFrequencyQueries>(provider => provider.GetRequiredService<MaintenanceFrequencyRepository>());

        services.AddScoped<MaintenanceScheduleRepository>();
        services.AddScoped<IMaintenanceScheduleRepository>(provider => provider.GetRequiredService<MaintenanceScheduleRepository>());
        services.AddScoped<IMaintenanceScheduleQueries>(provider => provider.GetRequiredService<MaintenanceScheduleRepository>());

        services.AddScoped<WorkOrderRepository>();
        services.AddScoped<IWorkOrderRepository>(provider => provider.GetRequiredService<WorkOrderRepository>());
        services.AddScoped<IWorkOrderQueries>(provider => provider.GetRequiredService<WorkOrderRepository>());

        services.AddScoped<WorkOrderPriorityRepository>();
        services.AddScoped<IWorkOrderPriorityRepository>(provider => provider.GetRequiredService<WorkOrderPriorityRepository>());
        services.AddScoped<IWorkOrderPriorityQueries>(provider => provider.GetRequiredService<WorkOrderPriorityRepository>());

        services.AddScoped<WorkOrderStatusRepository>();
        services.AddScoped<IWorkOrderStatusRepository>(provider => provider.GetRequiredService<WorkOrderStatusRepository>());
        services.AddScoped<IWorkOrderStatusQueries>(provider => provider.GetRequiredService<WorkOrderStatusRepository>());
    }
}
