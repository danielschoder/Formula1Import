using Formula1Import.Application.Handlers.QueryHandlers;
using Formula1Import.Application.Interfaces.Services;
using Formula1Import.Infrastructure.Helpers;
using MediatR;

namespace Formula1Import.Api.Endpoints;

public class AliveEndpoints : IEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/", GetVersionAsync);
        app.MapGet("/api", GetVersionAsync);
        app.MapGet("/error", ThrowError);

        static async Task<IResult> GetVersionAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new GetVersion.Query()));

        static IResult ThrowError(IScopedLogService logService)
        {
            logService.Log("Before error");
            var zero = 0;
            var y = 1 / zero;
            logService.Log("After error");
            return Results.Ok();
        }
    }
}
