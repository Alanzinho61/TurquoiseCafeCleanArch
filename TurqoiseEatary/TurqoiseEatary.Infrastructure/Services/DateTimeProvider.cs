using TurqoiseEatary.Application.Common.Interfaces.Services;

namespace TurqoiseEatary.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
