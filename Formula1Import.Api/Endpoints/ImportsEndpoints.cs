using Formula1Import.Application.Handlers.ImportCommandHandlers;
using Formula1Import.Application.Handlers.QueryHandlers;
using Formula1Import.Application.Interfaces.Services;
using Formula1Import.Contracts.F1Dtos;
using Formula1Import.Contracts.Responses;
using Formula1Import.Infrastructure.Helpers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.Json;

namespace Formula1Import.Api.Endpoints;

public class ImportsEndpoints : IEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/imports", GetImportsStatus);
        app.MapPost("/api/imports", ImportFromFile);
        //app.MapPost("/api/imports/start", ImportResultsAsync);
        //app.MapPost("/api/imports/stop", ImportResultsAsync);

        static IResult GetImportsStatus(IServiceBusListener serviceBusListener)
            => Results.Ok(new ImportsStatus
            {
                IsListenerActive = serviceBusListener.IsProcessing,
                NumberOfMessagesProcessed = serviceBusListener.NumberOfMessagesProcessed
            });

        static async Task<IResult> ImportFromFile(IMediator mediator, IConfiguration configuration, bool sprint = false)
        {
            var importRecords = await File.ReadAllLinesAsync(configuration["ImportFileName"], Encoding.Default);
            foreach (var importRecord in importRecords)
            {
                var f1Data = JsonSerializer.Deserialize<F1Data>(importRecord);
                if (f1Data.Races != null && f1Data.Races.Length > 0)
                {
                    await mediator.Send(new ImportRaces.Command(f1Data.Races));
                }
                if (f1Data.RaceResults != null)
                {
                    await mediator.Send(new ImportRaceResults.Command(f1Data.RaceResults, IsSprint: sprint));
                }
            }
            return Results.Ok(importRecords.Length);
        }
    }
}
