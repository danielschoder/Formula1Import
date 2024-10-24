namespace Formula1Import.Domain.Common.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }

    Task ForAllYears(int fromYear, int toYear, Func<int, Task> action);
}
