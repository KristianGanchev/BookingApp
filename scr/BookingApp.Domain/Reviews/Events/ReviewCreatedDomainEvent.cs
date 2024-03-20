using BookingApp.Domain.Abstractions;

namespace BookingApp.Domain.Reviews.Events;

public sealed record ReviewCreatedDomainEvent(Guid ReviewId) : IDomainEvent;