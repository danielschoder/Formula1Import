using Formula1Import.Application.Commands.ImportCommands;
using Formula1Import.Contracts.Requests;
using MediatR;

namespace Formula1Import.Api.Endpoints;

public static class RacesEndpoints
{
    public static void MapRacesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/races", ImportRacesAsync);

        static async Task<IResult> ImportRacesAsync(ImportRequest request, IMediator mediator)
            => Results.Ok(await mediator.Send(new ImportRacesCommand(request)));
    }
}
