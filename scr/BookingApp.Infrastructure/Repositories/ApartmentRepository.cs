using BookingApp.Domain.Apartments;

namespace BookingApp.Infrastructure.Repositories;

internal sealed class ApartmentRepository(ApplicationDbContext dbContext) 
    : Repository<Apartment>(dbContext), IApartmentRepository
{
}
