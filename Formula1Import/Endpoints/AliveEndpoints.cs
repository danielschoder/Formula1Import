using Formula1Import.Application.Interfaces.Services;
using Formula1Import.Application.Queries;
using MediatR;

namespace Formula1Import.Api.Endpoints;

public static class AliveEndpoints
{
    public static void MapAliveEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", GetVersionAsync).WithOpenApi();
        app.MapGet("/api", GetVersionAsync).WithOpenApi();
        app.MapGet("/error", ThrowError);

        static async Task<IResult> GetVersionAsync(IMediator mediator)
            => Results.Ok(await mediator.Send(new GetVersionQuery()));

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
