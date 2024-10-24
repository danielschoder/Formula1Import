using Formula1Import.Application.Commands.ImportCommands;
using Formula1Import.Contracts.Requests;
using MediatR;

namespace Formula1Import.Api.Endpoints;

public static class DriversEndpoints
{
    public static void MapDriversEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/drivers", ImportDriversAsync);

        static async Task<IResult> ImportDriversAsync(ImportRequest request, IMediator mediator)
            => Results.Ok(await mediator.Send(new ImportDriversCommand(request)));
    }
}
