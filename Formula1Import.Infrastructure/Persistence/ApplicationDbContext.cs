using Formula1.Application.Interfaces.Persistence;
using Formula1Import.Domain.Entities;
using Formula1Import.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Formula1Import.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
    : DbContext(options), IApplicationDbContext
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");

    public DbSet<Circuit> FORMULA1_Circuits { get; set; }

    public DbSet<Constructor> FORMULA1_Constructors { get; set; }

    public DbSet<Driver> FORMULA1_Drivers { get; set; }

    public DbSet<GrandPrix> FORMULA1_GrandPrix { get; set; }

    public DbSet<Race> FORMULA1_Races { get; set; }

    public DbSet<Result> FORMULA1_Results { get; set; }

    public DbSet<Season> FORMULA1_Seasons { get; set; }

    public DbSet<Session> FORMULA1_Sessions { get; set; }

    public DbSet<SessionType> FORMULA1_SessionTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        CircuitConfiguration.Configure(modelBuilder.Entity<Circuit>());
        ConstructorConfiguration.Configure(modelBuilder.Entity<Constructor>());
        DriverConfiguration.Configure(modelBuilder.Entity<Driver>());
        GrandPrixConfiguration.Configure(modelBuilder.Entity<GrandPrix>());
        RaceConfiguration.Configure(modelBuilder.Entity<Race>());
        ResultConfiguration.Configure(modelBuilder.Entity<Result>());
        SeasonConfiguration.Configure(modelBuilder.Entity<Season>());
        SessionConfiguration.Configure(modelBuilder.Entity<Session>());
        SessionTypeConfiguration.Configure(modelBuilder.Entity<SessionType>());

        base.OnModelCreating(modelBuilder);
    }
}
