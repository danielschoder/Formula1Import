using Formula1Import.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Formula1Import.Infrastructure.Services;

public class ExceptionServiceBase(
    IHttpContextAccessor httpContext,
    IScopedLogService logService)
{
    protected readonly IHttpContextAccessor _httpContext = httpContext;
    protected readonly IScopedLogService _logService = logService;

    protected async Task WriteResponse500Async<T>(T responseBody)
    {
        _httpContext.HttpContext.Response.StatusCode = 500;
        _httpContext.HttpContext.Response.ContentType = "application/json";
        await _httpContext.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(responseBody));
    }
}
