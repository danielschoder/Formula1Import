using Formula1Import.Application.Commands.ImportCommands;
using Formula1Import.Contracts.Requests;
using MediatR;

namespace Formula1Import.Api.Endpoints;

public static class GrandPrixEndpoints
{
    public static void MapGrandPrixEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/grandprix", ImportGrandPrixAsync);

        static async Task<IResult> ImportGrandPrixAsync(ImportRequest request, IMediator mediator)
            => Results.Ok(await mediator.Send(new ImportGrandPrixCommand(request)));
    }
}
