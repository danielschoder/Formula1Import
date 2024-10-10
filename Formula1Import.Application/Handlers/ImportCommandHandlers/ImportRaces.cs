using Formula1.Application.Interfaces.Persistence;
using Formula1Import.Application.Helpers;
using Formula1Import.Contracts.F1Dtos;
using Formula1Import.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1Import.Application.Handlers.ImportCommandHandlers;

public class ImportRaces(
    IApplicationDbContext context) : IRequestHandler<ImportRaces.Command>
{
    private readonly IApplicationDbContext _context = context;

    public record Command(F1Race[] Races) : IRequest;

    public async Task Handle(Command command, CancellationToken cancellationToken)
    {
        var importRaces = command.Races;
        var year = importRaces.First().Year;
        var grandPrixList = await GetGrandPrix();
        var races = await GetRacesOfYear();

        // Delete obsolete races from DB
        foreach (var race in races)
        {
            if (importRaces.FirstOrDefault(r => r.Round == race.Round) is null)
            {
                _context.FORMULA1_Races.Remove(race);
            }
        }

        // Add new grand prix to DB
        foreach (var importResultGroup in importRaces.GroupBy(r => r.GrandPrixName))
        {
            var grandPrixName = importResultGroup.Key;
            var grandPrixId = grandPrixList.ExistingId(grandPrixName);
            if (grandPrixId == 0)
            {
                var grandPrix = GrandPrix.Create(grandPrixName);
                _context.FORMULA1_GrandPrix.Add(grandPrix);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        grandPrixList = await GetGrandPrix();

        // Add/update races 
        foreach (var importRace in importRaces)
        {
            var race = races.FirstOrDefault(r => r.Round == importRace.Round);
            var grandPrixId = grandPrixList.ExistingId(importRace.GrandPrixName);
            // Add new race
            if (race is null)
            {
                race = new Race
                {
                    SeasonYear = importRace.Year,
                    Round = importRace.Round,
                    GrandPrixId = grandPrixId,
                    CircuitId = null
                };
                _context.FORMULA1_Races.Add(race);
                continue;
            }
            // Upate race
            if (race.GrandPrixId != grandPrixId)
            {
                race.GrandPrixId = grandPrixId;
                _context.FORMULA1_Races.Update(race);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        async Task<Dictionary<string, int>> GetGrandPrix()
            => await _context.FORMULA1_GrandPrix
                .ToDictionaryAsync(d => d.Name, d => d.Id, cancellationToken);

        async Task<List<Race>> GetRacesOfYear()
            => await _context.FORMULA1_Races
                .AsNoTracking()
                .Where(r => r.SeasonYear == year)
                .ToListAsync(cancellationToken);
    }
}
