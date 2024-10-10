using Azure.Messaging.ServiceBus;
using Formula1Import.Application.Interfaces.Services;
using Formula1Import.Domain.Common.Interfaces;
using Formula1Import.Infrastructure.Services;

namespace Formula1Import.Api.ServiceRegistrations;

public static class InfrastructureServiceRegistrations
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IWebHostEnvironment environment,
        IConfiguration configuration)
    {
        var isDevelopment = environment.IsDevelopment();

        services.AddHttpContextAccessor();

        services.AddSingleton(serviceProvider =>
        {
            var connectionString = configuration["ServiceBus:ConnectionString"];
            return new ServiceBusClient(connectionString);
        });
        services.AddSingleton<IServiceBusListener, ServiceBusListener>();
        services.AddHostedService(provider => (ServiceBusListener)provider.GetRequiredService<IServiceBusListener>());

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IScopedErrorService, ScopedErrorService>();
        services.AddScoped<IScopedLogService, ScopedLogService>();

        services.AddScoped(typeof(IExceptionService),
            isDevelopment ? typeof(ExceptionInDevelopmentService) : typeof(ExceptionInProductionService));

        return services;
    }
}
