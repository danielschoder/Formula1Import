using Formula1.Application.Interfaces.Persistence;
using Formula1Import.Application.Commands.ImportCommands;
using MediatR;

namespace Formula1Import.Application.Handlers.ImportCommandHandlers;

public class ImportCircuitsCommandHandler(IApplicationDbContext context)
    : IRequestHandler<ImportCircuitsCommand, Unit>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Unit> Handle(ImportCircuitsCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new Unit());
    }
}
