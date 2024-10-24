using Formula1Import.Application.Queries;
using Formula1Import.Contracts.Responses;
using Formula1Import.Domain.Common.Interfaces;
using MediatR;
using System.Reflection;

namespace Formula1Import.Application.Handlers.QueryHandlers;

public class GetVersionQueryHandler(IDateTimeProvider dateTimeProvider) : IRequestHandler<GetVersionQuery, Alive>
{
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public Task<Alive> Handle(GetVersionQuery request, CancellationToken cancellationToken)
        => Task.FromResult(new Alive
        {
            UtcNow = _dateTimeProvider.UtcNow,
            Version = Assembly.GetEntryAssembly().GetName().Version.ToString()
        });
}
