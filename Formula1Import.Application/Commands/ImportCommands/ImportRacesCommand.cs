using Formula1Import.Contracts.Requests;
using MediatR;

namespace Formula1Import.Application.Commands.ImportCommands;

public class ImportRacesCommand(ImportRequest importRequest) : IRequest<Unit>
{
    public int FromYear { get; set; } = importRequest.FromYear;
    public int ToYear { get; set; } = importRequest.ToYear;
}
