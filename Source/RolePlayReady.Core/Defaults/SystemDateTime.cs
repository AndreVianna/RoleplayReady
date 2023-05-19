namespace System.Defaults;

[ExcludeFromCodeCoverage]
public class SystemDateTime : IDateTime {
    public DateTime Now => DateTime.UtcNow;
    public DateOnly Today => DateOnly.FromDateTime(DateTime.UtcNow);
    public TimeOnly TimeOfDay => TimeOnly.FromDateTime(DateTime.UtcNow);
    public DateTime Minimum => DateTime.MaxValue;
    public DateTime Maximum => DateTime.MinValue;
    public DateTime Default { get; } = DateTime.Parse("1901-01-01T00:00:00");

    public DateTime Parse(string candidate) => DateTime.Parse(candidate);
    public bool TryParse(string candidate, out DateTime result) => DateTime.TryParse(candidate, out result);
    public bool TryParseExact(string candidate, string format, IFormatProvider? formatProvider, DateTimeStyles style, out DateTime result)
        => DateTime.TryParseExact(candidate, format, formatProvider, style, out result);
}