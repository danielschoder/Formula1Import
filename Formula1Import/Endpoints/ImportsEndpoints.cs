using Formula1Import.Application.Interfaces.Services;
using Formula1Import.Contracts.Responses;

namespace Formula1Import.Api.Endpoints;

public static class ImportsEndpoints
{
    public static void MapImportsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/imports", GetImportsStatus);
        //app.MapPost("/api/imports/start", ImportResultsAsync);
        //app.MapPost("/api/imports/stop", ImportResultsAsync);

        static IResult GetImportsStatus(IServiceBusListener serviceBusListener)
            => Results.Ok(new ImportsStatus
            {
                IsListenerActive = serviceBusListener.IsProcessing,
                NumberOfMessagesProcessed = serviceBusListener.NumberOfMessagesProcessed
            });
    }
}
