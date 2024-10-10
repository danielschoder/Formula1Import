namespace Formula1Import.Application.Interfaces.Services;

public interface IExceptionService
{
    Task HandleExceptionAsync(Exception exception);
}
