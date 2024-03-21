using BookingApp.Application.Abstractions.Messaging;

namespace BookingApp.Application.Apartments.SearchApartment;

public sealed record SearchApartmentQuery(
    DateOnly StartDate,
    DateOnly EndDate) : IQuery<IReadOnlyList<ApartmentResponse>>;