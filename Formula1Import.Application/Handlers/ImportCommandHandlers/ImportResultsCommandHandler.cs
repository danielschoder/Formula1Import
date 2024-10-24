using Formula1.Application.Interfaces.Persistence;
using Formula1Import.Application.Commands.ImportCommands;
using Formula1Import.Contracts.F1Dtos;
using Formula1Import.Domain.Entities;
using Formula1Import.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;

namespace Formula1Import.Application.Handlers.ImportCommandHandlers;

public class ImportResultsCommandHandler(
    IApplicationDbContext context)
    :  IRequestHandler<ImportResultsCommand>
{
    private readonly IApplicationDbContext _context = context;

    public async Task Handle(ImportResultsCommand importResultsCommand, CancellationToken cancellationToken)
    {
        var raceResults = importResultsCommand.RaceResults;
        var drivers = await _context.FORMULA1_Drivers.ToDictionaryAsync(e => e.Name, e => e.Id, cancellationToken);
        var constructors = await _context.FORMULA1_Constructors.ToDictionaryAsync(e => e.Name, e => e.Id, cancellationToken);
        var race = await _context.FORMULA1_Races
            .Where(r => r.SeasonYear.Equals(raceResults.Year) && r.Round.Equals(raceResults.Round))
            .Include(r => r.Sessions.Where(s => s.SessionType.Id.Equals(SessionTypeEnum.Race)))
                .ThenInclude(s => s.Results)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new Exception("Race for this year/round not found.");

        if (race.Sessions is null || race.Sessions.Count == 0)
        {
            race.Sessions.Add(Session.Create(0, race.Id));
        }
        var session = race.Sessions.First();
        var results = session.Results.ToDictionary(e => e.DriverId);

        foreach (var importResult in importResultsCommand.RaceResults.Results)
        {
            importResult.DriverName = FormatDriverName(importResult.DriverName);
            var driverId = drivers[importResult.DriverName];
            var constructorId = constructors[importResult.ConstructorName];
            results.TryGetValue(driverId, out var existingResult);
            UpdateResult(existingResult ?? InsertResult(session), importResult, driverId, constructorId);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return;

        void UpdateResult(Result result, F1Result importResult, Guid driverId, Guid constructorId)
        {
            int.TryParse(importResult.Position, out var position);
            result.Position = position == 0 ? 99 : position;
            result.Points = importResult.Points;
            result.DriverId = driverId;
            result.ConstructorId = constructorId;
        }

        Result InsertResult(Session session)
        {
            var result = Result.Create(session.Id);
            session.Results.Add(result);
            return result;
        }

        string FormatDriverName(string name)
        {
            name = Regex.Replace(WebUtility.HtmlDecode(name).Trim(), @"\s+", " ");
            return name.Length >= 3 && name[^3..].All(char.IsUpper)
                        ? name.Substring(0, name.Length - 3)
                        : name;
        }
    }
}
