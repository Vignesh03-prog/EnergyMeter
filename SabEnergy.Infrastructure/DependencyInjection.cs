using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Application.Modules.Services.Masters;
using SabEnergy.Infrastructure.Persistence;
using SabEnergy.Infrastructure.Repositories.Masters;

namespace SabEnergy.Infrastructure;

public class DIServices
{
    public static void InjectServices(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
            ?? configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string not configured");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
            }));

        // Repositories (Infrastructure)
        services.AddScoped<IGatewayMakeRepository, GatewayMakeRepository>();
        services.AddScoped<IMeterMakeRepository, MeterMakeRepository>();
        services.AddScoped<IGatewayModelRepository, GatewayModelRepository>();
        services.AddScoped<IMeterModelRepository, MeterModelRepository>();
        services.AddScoped<IGatewayRepository, GatewayRepository>();
        services.AddScoped<IMeterRepository, MeterRepository>();
        services.AddScoped<IMeterProfileRepository, MeterProfileRepository>();
        services.AddScoped<IParameterMasterRepository, ParameterMasterRepository>();
        services.AddScoped<IMeterParameterRepository, MeterParameterRepository>();

        // Services (Application)
        services.AddScoped<IGatewayMakeService, GatewayMakeService>();
        services.AddScoped<IMeterMakeService, MeterMakeService>();
        services.AddScoped<IGatewayModelService, GatewayModelService>();
        services.AddScoped<IMeterModelService, MeterModelService>();
        services.AddScoped<IGatewayService, GatewayService>();
        services.AddScoped<IMeterService, MeterService>();
        services.AddScoped<IMeterProfileService, MeterProfileService>();
        services.AddScoped<IParameterMasterService, ParameterMasterService>();
        services.AddScoped<IMeterParameterService, MeterParameterService>();
    }
}
