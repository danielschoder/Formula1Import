using Formula1Import.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Application.Interfaces.Persistence;

public interface IApplicationDbContext
{
    public DbSet<Circuit> FORMULA1_Circuits { get; set; }

    public DbSet<Constructor> FORMULA1_Constructors { get; set; }

    public DbSet<Driver> FORMULA1_Drivers { get; set; }

    public DbSet<GrandPrix> FORMULA1_GrandPrix { get; set; }

    public DbSet<Race> FORMULA1_Races { get; set; }

    public DbSet<Result> FORMULA1_Results { get; set; }

    public DbSet<Season> FORMULA1_Seasons { get; set; }

    public DbSet<Session> FORMULA1_Sessions { get; set; }

    public DbSet<SessionType> FORMULA1_SessionTypes { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
