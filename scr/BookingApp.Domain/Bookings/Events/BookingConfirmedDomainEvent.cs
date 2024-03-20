using BookingApp.Domain.Abstractions;

namespace BookingApp.Domain.Bookings.Events;

public record BookingConfirmedDomainEvent(Guid BookingId) : IDomainEvent;