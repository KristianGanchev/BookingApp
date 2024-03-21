namespace BookingApp.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendAsync(Domain.Users.Email message, string subject, string body);
}
