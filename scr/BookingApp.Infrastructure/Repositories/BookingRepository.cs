using BookingApp.Domain.Apartments;
using BookingApp.Domain.Bookings;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Infrastructure.Repositories;

internal sealed class BookingRepository(ApplicationDbContext dbContext) : Repository<Booking>(dbContext), IBookingRepository
{
    private static readonly BookingStatus[] ActiveBookingStatuses =
    {
        BookingStatus.Reserved,
        BookingStatus.Confirmed,
        BookingStatus.Completed
    };

    public async Task<bool> IsOverlappingAsync(Apartment apartment, DateRange duration, CancellationToken cancellationToken = default) =>
        await DbContext
            .Set<Booking>()
            .AnyAsync(booking =>
                booking.ApartmentId == apartment.Id &&
                booking.Duration.Start <= duration.End &&
                booking.Duration.End >= duration.Start &&
                ActiveBookingStatuses.Contains(booking.Status), 
                cancellationToken);
}
