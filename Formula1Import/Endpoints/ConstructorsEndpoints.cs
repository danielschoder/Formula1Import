using Formula1Import.Application.Commands.ImportCommands;
using Formula1Import.Contracts.Requests;
using MediatR;

namespace Formula1.Api.Endpoints;

public static class ConstructorsEndpoints
{
    public static void MapConstructorsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/constructors", ImportConstructorsAsync);

        static async Task<IResult> ImportConstructorsAsync(ImportRequest request, IMediator mediator)
            => Results.Ok(await mediator.Send(new ImportConstructorsCommand(request)));
    }
}
