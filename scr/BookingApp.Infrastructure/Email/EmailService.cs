using BookingApp.Application.Abstractions.Email;

namespace BookingApp.Infrastructure.Email;

internal sealed class EmailService : IEmailService
{
    public Task SendAsync(Domain.Users.Email message, string subject, string body)
    {
        return Task.CompletedTask;
    }
}
