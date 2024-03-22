using BookingApp.Domain.Users;

namespace BookingApp.Infrastructure.Repositories;

internal sealed class UserRepository(ApplicationDbContext dbContext) 
    : Repository<User>(dbContext), IUserRepository
{
}
