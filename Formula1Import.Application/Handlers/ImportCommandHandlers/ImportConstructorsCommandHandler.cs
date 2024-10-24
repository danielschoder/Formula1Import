using Formula1.Application.Interfaces.Persistence;
using Formula1Import.Application.Commands.ImportCommands;
using Formula1Import.Contracts.F1Dtos;
using Formula1Import.Contracts.Responses;
using Formula1Import.Domain.Common.Interfaces;
using Formula1Import.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formula1Import.Application.Handlers.ImportCommandHandlers;

public class ImportConstructorsCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<ImportConstructorsCommand, ImportResponse>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<ImportResponse> Handle(ImportConstructorsCommand request, CancellationToken cancellationToken)
    {
        var response = new ImportResponse(request.GetType().Name, request.FromYear, request.ToYear);
        await _dateTimeProvider.ForAllYears(request.FromYear, request.ToYear, async year =>
        {
            await UpdateInsertConstructors(
                await _context.FORMULA1_Constructors.ToDictionaryAsync(e => e.Name, cancellationToken),
                null,
                year,
                response,
                cancellationToken);
        });

        return response;

        async Task UpdateInsertConstructors(
            Dictionary<string, Constructor> constructors,
            List<F1Constructor> importConstructors,
            int year,
            ImportResponse reponse,
            CancellationToken cancellationToken)
        {
            foreach (var importConstructor in importConstructors)
            {
                response.RowsInDatabase = constructors.Count;
                response.UniqueRowsInImport = importConstructors.Count;
                constructors.TryGetValue(importConstructor.Name, out var existingConstructor);
                response.RowsInserted += existingConstructor is null ? 1 : 0;
                UpdateConstructor(existingConstructor ?? await InsertConstructor(_context, cancellationToken), importConstructor);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        void UpdateConstructor(Constructor constructor, F1Constructor importConstructor)
            => constructor.Name = importConstructor.Name;

        async Task<Constructor> InsertConstructor(IApplicationDbContext context, CancellationToken cancellationToken)
            => (await context.FORMULA1_Constructors.AddAsync(Constructor.Create(), cancellationToken)).Entity;
    }
}
