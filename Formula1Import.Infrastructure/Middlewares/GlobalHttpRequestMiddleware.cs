using Formula1Import.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Formula1Import.Infrastructure.Middlewares;

public class GlobalHttpRequestMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, IScopedLogService logService)
    {
        var requestUrl = $"{context.Request.Method} {context.Request.Path}";
        logService.Log(requestUrl, nameof(requestUrl));

        await _next(context);
    }
}
