using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SabEnergy.Domain.Modules.Masters;
using SabEnergy.Domain.Modules.Masters.Model;

namespace SabEnergy.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<GatewayMake> GatewayMakes { get; set; }
    public DbSet<MeterMake> MeterMakes { get; set; }
    public DbSet<GatewayModel> GatewayModels { get; set; }
    public DbSet<MeterModel> MeterModels { get; set; }
    public DbSet<Gateway> Gateways { get; set; }
    public DbSet<Meter> Meters { get; set; }
    public DbSet<MeterProfile> MeterProfiles { get; set; }
    public DbSet<ParameterMaster> ParameterMasters { get; set; }
    public DbSet<MeterParameter> MeterParameters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
