using Formula1Import.Domain.Common.Interfaces;

namespace Formula1Import.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;

    public async Task ForAllYears(int fromYear, int toYear, Func<int, Task> action)
    {
        fromYear = fromYear > 0 ? fromYear : 1950;
        toYear = toYear < 9999 ? toYear : UtcNow.Year;
        for (int y = fromYear; y <= toYear; y++)
        {
            await action(y);
        }
    }
}
