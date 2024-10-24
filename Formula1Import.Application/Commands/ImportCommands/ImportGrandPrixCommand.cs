using Formula1Import.Contracts.Requests;
using Formula1Import.Contracts.Responses;
using MediatR;

namespace Formula1Import.Application.Commands.ImportCommands;

public class ImportGrandPrixCommand(ImportRequest importRequest) : IRequest<ImportResponse>
{
    public int FromYear { get; set; } = importRequest.FromYear;
    public int ToYear { get; set; } = importRequest.ToYear;
}
