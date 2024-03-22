using BookingApp.Application.Abstractions.Clock;

namespace BookingApp.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
