using Formula1Import.Contracts.F1Dtos;
using MediatR;

namespace Formula1Import.Application.Commands.ImportCommands;

public class ImportResultsCommand(F1Race raceResults) : IRequest
{
    public F1Race RaceResults { get; set; } = raceResults;
}
