using Azure.Messaging.ServiceBus;
using Formula1Import.Application.Commands.ImportCommands;
using Formula1Import.Application.Interfaces.Services;
using Formula1Import.Contracts.F1Dtos;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace Formula1Import.Infrastructure.Services;

public class ServiceBusListener(
    IServiceScopeFactory serviceScopeFactory,
    ServiceBusClient serviceBusClient,
    IConfiguration configuration)
    : IServiceBusListener, IHostedService
{
    //private readonly IMediator _mediator = mediator;
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
    private readonly ServiceBusClient _serviceBusClient = serviceBusClient;
    private readonly ServiceBusProcessor _serviceBusProcessor
        = serviceBusClient.CreateProcessor(configuration["ServiceBus:QueueName"], new ServiceBusProcessorOptions());
    private int _numberOfMessagesProcessed;

    public bool IsProcessing => _serviceBusProcessor.IsProcessing;

    public int NumberOfMessagesProcessed => _numberOfMessagesProcessed;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _serviceBusProcessor.ProcessMessageAsync += MessageHandler;
        _serviceBusProcessor.ProcessErrorAsync += ErrorHandler;
        await _serviceBusProcessor.StartProcessingAsync(cancellationToken);

        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine($"Error processing message: {args.Exception.Message}");
            return Task.CompletedTask;
        }
    }

    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        try
        {
            var raceResults = JsonSerializer.Deserialize<F1Race>(args.Message.Body.ToString());

            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(new ImportResultsCommand(raceResults));
        }
        catch
        {
            await _serviceBusProcessor.StopProcessingAsync();
            throw;
        }
        await args.CompleteMessageAsync(args.Message);
        _numberOfMessagesProcessed++;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _serviceBusProcessor.StopProcessingAsync(cancellationToken);
        await _serviceBusProcessor.DisposeAsync();
        await _serviceBusClient.DisposeAsync();
    }
}
