using Formula1Import.Application.Interfaces.Services;
using Formula1Import.Contracts.Responses;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Formula1Import.Infrastructure.Services;

public class ExceptionInDevelopmentService(
    IHttpContextAccessor httpContext,
    IScopedLogService logService)
    : ExceptionServiceBase(httpContext, logService), IExceptionService
{
    public async Task HandleExceptionAsync(Exception exception)
    {
        await WriteResponse500Async(new ExceptionResponse(exception.Message, _logService.GetLogsAsList()));

        Debug.WriteLine(string.Empty);
        Debug.WriteLine("== ERROR ==");
        Debug.WriteLine(_logService.ExceptionAsTextBlock(exception));
        Debug.WriteLine("===========");
        Debug.WriteLine(string.Empty);
    }
}
