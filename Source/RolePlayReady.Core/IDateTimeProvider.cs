using System.Globalization;

namespace RolePlayReady;

public interface IDateTimeProvider {
    DateTime Now { get; }
    DateOnly Today { get; }
    TimeOnly TimeOfDay { get; }
    DateTime First { get; }
    DateTime Last { get; }
    DateTime Parse(string candidate);
    bool TryParse(string candidate, out DateTime result);
    bool TryParseExact(string candidate, string format, IFormatProvider? formatProvider, DateTimeStyles style, out DateTime result);
}