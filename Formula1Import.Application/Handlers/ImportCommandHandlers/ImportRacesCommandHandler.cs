using Formula1.Application.Interfaces.Persistence;
using Formula1Import.Application.Commands.ImportCommands;
using Formula1Import.Domain.Common.Interfaces;
using MediatR;

namespace Formula1Import.Application.Handlers.ImportCommandHandlers;

public class ImportRacesCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<ImportRacesCommand, Unit>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<Unit> Handle(ImportRacesCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new Unit());
    }
}
