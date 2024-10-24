namespace Formula1Import.Application.Interfaces.Services;

public interface IServiceBusListener
{
    bool IsProcessing { get; }
 
    int NumberOfMessagesProcessed { get; }

    Task StartAsync(CancellationToken cancellationToken);

    Task StopAsync(CancellationToken cancellationToken);
}
