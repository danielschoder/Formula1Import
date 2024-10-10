using Formula1Import.Application.Interfaces.Services;

namespace Formula1Import.Infrastructure.Services;

public class ScopedErrorService : IScopedErrorService
{
    public List<string> Errors { get; } = [];

    public void AddError(string message)
        => Errors.Add(message);

    public void AddErrorIf(bool condition, string message)
    {
        if (condition)
        {
            AddError(message);
        }
    }

    public T AddNotFoundError<T>(string key) where T : class
    {
        AddError($"Resource {typeof(T).Name} not found for '{key}'.");
        return default;
    }
}
