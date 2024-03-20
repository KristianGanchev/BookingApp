using BookingApp.Domain.Abstractions;

namespace BookingApp.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;
