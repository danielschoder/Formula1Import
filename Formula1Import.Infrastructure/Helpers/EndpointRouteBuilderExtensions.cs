using Microsoft.AspNetCore.Routing;
using System.Reflection;

namespace Formula1Import.Infrastructure.Helpers;

public static class EndpointRouteBuilderExtensions
{
    public static void MapAllEndpoints(this IEndpointRouteBuilder app)
    {
        var endpointsMaps = Assembly.GetEntryAssembly().GetTypes()
            .Where(t => typeof(IEndpoints).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

        foreach (var endpointsMap in endpointsMaps)
        {
            var method = endpointsMap.GetMethod("MapEndpoints", BindingFlags.Static | BindingFlags.Public);
            method?.Invoke(null, [app]);
        }
    }
}
