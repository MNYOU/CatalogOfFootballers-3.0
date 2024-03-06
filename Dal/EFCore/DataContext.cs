using Dal.EFCore.Configurations;
using Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Dal.EFCore;

public class DataContext : DbContext
{
    private readonly IConfiguration configuration;

    public DataContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public DbSet<Team> Teams { get; set; }

    public DbSet<Player> Footballers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = configuration.GetConnectionString("Postgresql");
        optionsBuilder.UseNpgsql(config);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("FootballersCatalog");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlayerConfiguration).Assembly);
    }
}