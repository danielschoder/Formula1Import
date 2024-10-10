using Formula1.Application.Interfaces.Persistence;
using Formula1Import.Application.Helpers;
using Formula1Import.Contracts.F1Dtos;
using Formula1Import.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1Import.Application.Handlers.ImportCommandHandlers;

public class ImportRaceResults(
    IApplicationDbContext context)
    :  IRequestHandler<ImportRaceResults.Command>
{
    private readonly IApplicationDbContext _context = context;

    public record Command(F1RaceResults RaceResults, bool IsSprint = false) : IRequest;

    public async Task Handle(Command command, CancellationToken cancellationToken)
    {
        var importRaceResults = command.RaceResults;
        var importCircuitName = importRaceResults.CircuitName;
        var importResults = importRaceResults.Results;
        var drivers = await GetDrivers();
        var constructors = await GetConstructors();
        var circuits = command.IsSprint ? null : await GetCircuits();
        var sessionType = command.IsSprint ? 4 : 0;
        var race = await GetRace(sessionType);
        var session = race.Sessions.FirstOrDefault();
        var results = session?.Results ?? [];

        // Calulcate positions
        var position = 0;
        foreach (var result in importResults)
        {
            result.Position = ++position;
        }

        // Delete obsolete results from DB
        foreach (var result in results)
        {
            if (importResults
                .FirstOrDefault(r => r.Position == result.Position) is null)
            {
                _context.FORMULA1_Results.Remove(result);
            }
        }

        // Add new drivers to DB
        foreach (var importResultGroup in importResults.GroupBy(r => r.DriverName))
        {
            var driverName = importResultGroup.Key;
            var driverId = drivers.ExistingId(driverName);
            if (driverId == 0)
            {
                var driver = Driver.Create(driverName);
                _context.FORMULA1_Drivers.Add(driver);
            }
        }

        // Add new constructors to DB
        foreach (var importResultGroup in importResults.GroupBy(r => r.ConstructorName))
        {
            var constructorName = importResultGroup.Key;
            var constructorId = constructors.ExistingId(constructorName);
            if (constructorId == 0)
            {
                var constructor = Constructor.Create(constructorName);
                _context.FORMULA1_Constructors.Add(constructor);
            }
        }

        // Add new circuit to DB - only for race result imports, not e.g. sprints
        if (sessionType == 0)
        {
            var circuitId = circuits.ExistingId(importCircuitName);
            if (circuitId == 0)
            {
                var circuit = Circuit.Create(importCircuitName);
                _context.FORMULA1_Circuits.Add(circuit);
                race.Circuit = circuit;
            }
            else
            {
                race.CircuitId = circuitId;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        drivers = await GetDrivers();
        constructors = await GetConstructors();

        // Add new session to DB
        if (session is null)
        {
            session = new Session
            {
                SessionTypeId = sessionType,
                RaceId = race.Id,
                Results = []
            };
            race.Sessions.Add(session);
            _context.FORMULA1_Sessions.Add(session);
        }

        // Add/update results 
        results = session.Results;
        foreach (var importResult in importResults)
        {
            var importPosition = importResult.Position;
            var result = results.FirstOrDefault(r => r.Position == importPosition);
            var driverId = drivers.ExistingId(importResult.DriverName);
            var construcorId = constructors.ExistingId(importResult.ConstructorName);
            // Add new result
            if (result is null)
            {
                result = new Result
                {
                    Session = session,
                    DriverId = driverId,
                    ConstructorId = construcorId,
                    Position = importPosition,
                    Ranking = importResult.Ranking,
                    Points = importResult.Points,
                };
                _context.FORMULA1_Results.Add(result);
                continue;
            }
            // Upate result
            if (result.DriverId != driverId
                || result.ConstructorId != construcorId
                || result.Ranking != importResult.Ranking
                || result.Points != importResult.Points)
            {
                result.DriverId = driverId;
                result.ConstructorId = construcorId;
                result.Ranking = importResult.Ranking;
                result.Points = importResult.Points;
                _context.FORMULA1_Results.Update(result);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        async Task<Dictionary<string, int>> GetDrivers()
            => await _context.FORMULA1_Drivers.ToDictionaryAsync(d => d.Name, d => d.Id, cancellationToken);

        async Task<Dictionary<string, int>> GetConstructors()
            => await _context.FORMULA1_Constructors.ToDictionaryAsync(d => d.Name, d => d.Id, cancellationToken);

        async Task<Dictionary<string, int>> GetCircuits()
            => await _context.FORMULA1_Circuits.ToDictionaryAsync(d => d.Name, d => d.Id, cancellationToken);

        async Task<Race> GetRace(int sessionType)
            => await _context.FORMULA1_Races
                .Where(r => r.SeasonYear.Equals(importRaceResults.Year) && r.Round.Equals(importRaceResults.Round))
                .Include(r => r.Sessions.Where(s => s.SessionTypeId == sessionType))
                    .ThenInclude(s => s.Results)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new Exception("Race for this year/round not found.");
    }
}
