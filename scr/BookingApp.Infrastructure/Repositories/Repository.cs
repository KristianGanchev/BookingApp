using BookingApp.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Infrastructure.Repositories;

internal abstract class Repository<T>(ApplicationDbContext dbContext)
    where T : Entity
{
    protected readonly ApplicationDbContext DbContext = dbContext;

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await DbContext.Set<T>().FirstOrDefaultAsync(user => user.Id == id, cancellationToken: cancellationToken);

    public void Add(T entity) => 
        DbContext.Add(entity);
}
