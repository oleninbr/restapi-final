using WebAPI.Filters;
using Application.Common.Settings;
using FluentValidation;
using WebAPI.Services.Abstract;
using WebAPI.Services.Implementation;

namespace WebAPI.Modules;

public static class SetupModule
{
    public static void SetupServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
        });
        services.AddCors();
        services.AddRequestValidators();
        services.AddApplicationSettings(configuration);
        services.AddControllerServices();

    }

    private static void AddCors(this IServiceCollection services)
    {
        services.AddCors(options =>
            options.AddDefaultPolicy(policy =>
                policy.SetIsOriginAllowed(_ => true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()));
    }

    private static void AddRequestValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<Program>();
    }

    private static void AddApplicationSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.Get<ApplicationSettings>();
        if (settings != null)
        {
            services.AddSingleton(settings);
        }
    }

    private static void AddControllerServices(this IServiceCollection services)
    {
        services.AddScoped<IСonditionerTypesControlerService, СonditionerTypesControlerService>();
        services.AddScoped<IСonditionerStatusesControllerService, СonditionerStatusesControllerService>();
        services.AddScoped<IManufacturersControllerService, ManufacturersControllerService>();
        services.AddScoped<IWorkOrderStatusesControllerService, WorkOrderStatusesControllerService>();
    }


}