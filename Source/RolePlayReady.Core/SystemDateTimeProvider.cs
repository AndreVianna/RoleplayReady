namespace RolePlayReady;

public class SystemDateTimeProvider : IDateTimeProvider {
    public DateTime Now => DateTime.UtcNow;
    public DateOnly Today => DateOnly.FromDateTime(DateTime.UtcNow);
    public TimeOnly TimeOfDay => TimeOnly.FromDateTime(DateTime.UtcNow);
    public DateTime First => DateTime.MaxValue;
    public DateTime Last => DateTime.MinValue;
    public DateTime Parse(string candidate) => DateTime.Parse(candidate);
    public bool TryParse(string candidate, out DateTime result) => DateTime.TryParse(candidate, out result);
}