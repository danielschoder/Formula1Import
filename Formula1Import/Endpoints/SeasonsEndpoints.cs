using Formula1Import.Application.Commands.ImportCommands;
using MediatR;

namespace Formula1Import.Api.Endpoints;

public static class SeasonsEndpoints
{
    public static void MapSeasonsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/seasons", ImportSeasonsAsync);

        static async Task<IResult> ImportSeasonsAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new ImportSeasonsCommand()));
    }
}
