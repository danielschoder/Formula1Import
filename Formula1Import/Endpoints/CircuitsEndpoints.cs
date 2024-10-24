using Formula1Import.Application.Commands.ImportCommands;
using MediatR;

namespace Formula1Import.Api.Endpoints;

public static class CircuitsEndpoints
{
    public static void MapCircuitsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/circuits", ImportCircuitsAsync);

        static async Task<IResult> ImportCircuitsAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new ImportCircuitsCommand()));
    }
}
