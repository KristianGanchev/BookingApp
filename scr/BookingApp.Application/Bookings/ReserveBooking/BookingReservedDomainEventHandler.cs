using BookingApp.Application.Abstractions.Email;
using BookingApp.Domain.Bookings;
using BookingApp.Domain.Bookings.Events;
using BookingApp.Domain.Users;
using MediatR;

namespace BookingApp.Application.Bookings.ReserveBooking;

internal sealed class BookingReservedDomainEventHandler(
    IBookingRepository bookingRepository,
    IUserRepository userRepository,
    IEmailService emailService)
    : INotificationHandler<BookingReservedDomainEvent>
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IEmailService _emailService = emailService;

    public async Task Handle(BookingReservedDomainEvent notification, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(notification.BookingId, cancellationToken);

        if (booking is null) 
        { 
            return;
        }

        var user = await _userRepository.GetByIdAsync(booking.UserId, cancellationToken);

        if (user is null)
        {
            return;
        }

        await _emailService.SendAsync(user.Email, "Booking reserved!", $"Booking with id {booking.Id} has been reserved");
    }
}
