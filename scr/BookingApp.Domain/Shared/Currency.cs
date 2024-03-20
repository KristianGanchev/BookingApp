namespace BookingApp.Domain.Shared;

public record Currency
{
    internal static readonly Currency None = new(string.Empty);
    public static readonly Currency Usd = new("USD");
    public static readonly Currency Eur = new("EUR");
    public static readonly Currency Bgn = new("BGN");

    private Currency(string code) => Code = code;

    public string Code { get; init; }

    public static Currency FromCode(string code) =>
        All.FirstOrDefault(c => c.Code == code) ??
            throw new ApplicationException("The currency is invalid");


    public static readonly IReadOnlyCollection<Currency> All = [Usd, Eur, Bgn];
}
