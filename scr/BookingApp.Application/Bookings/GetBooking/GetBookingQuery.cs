using BookingApp.Application.Abstractions.Messaging;

namespace BookingApp.Application.Bookings.GetBooking;

public sealed record GetBookingQuery(Guid BookingId) : IQuery<BookingResponse>;