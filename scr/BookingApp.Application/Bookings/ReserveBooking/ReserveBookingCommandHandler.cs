using BookingApp.Application.Abstractions.Clock;
using BookingApp.Application.Abstractions.Messaging;
using BookingApp.Application.Exceptions;
using BookingApp.Domain.Abstractions;
using BookingApp.Domain.Apartments;
using BookingApp.Domain.Bookings;
using BookingApp.Domain.Users;

namespace BookingApp.Application.Bookings.ReserveBooking;

internal sealed class ReserveBookingCommandHandler(
    IUserRepository userRepository, 
    IBookingRepository bookingRepository, 
    IApartmentRepository apartmentRepository, 
    IUnitOfWork unitOfWork,
    PricingService pricingService,
    IDateTimeProvider dateTimeProvider) 
    : ICommandHandler<ReserveBookingCommand, Guid>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly IApartmentRepository _apartmentRepository = apartmentRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly PricingService _pricingService = pricingService;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        var apartment = await _apartmentRepository.GetByIdAsync(request.ApartmentId, cancellationToken);

        if (apartment is null)
        {
            return Result.Failure<Guid>(ApartmentErrors.NotFound);
        }

        var duration = DateRange.Create(request.StartDate, request.EndDate);

        if (await _bookingRepository.IsOverlappingAsync(apartment, duration, cancellationToken))
        {
            return Result.Failure<Guid>(BookingErrors.Overlap);
        }

        try
        {
            var booking = Booking.Reserve(
            apartment,
            user.Id,
            duration,
            _dateTimeProvider.UtcNow,
            _pricingService);

            _bookingRepository.Add(booking);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return booking.Id;
        }
        catch (ConcurrencyException)
        {
            return Result.Failure<Guid>(BookingErrors.Overlap);
        }
    }
}
