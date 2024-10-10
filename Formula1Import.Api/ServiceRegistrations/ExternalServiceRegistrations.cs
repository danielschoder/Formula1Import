using Formula1Import.Contracts.ExternalServices;
using Formula1Import.Infrastructure.ExternalApis;

namespace Formula1Import.Api.ServiceRegistrations;

public static class ExternalServiceRegistrations
{
    public static IServiceCollection AddExternalServices(this IServiceCollection services)
    {
        services.AddHttpClient<ISlackClient, SlackClient>(client =>
        {
            client.BaseAddress = new Uri("https://hooks.slack.com");
        });

        return services;
    }
}
