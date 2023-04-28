namespace System.Utilities;

public record struct Base64Guid(Guid Value) {

    public Base64Guid(string? value)
        : this(ToGuid(value)) {
    }

    public static implicit operator Base64Guid(string value) => new(value);
    public static implicit operator Base64Guid(Guid value) => new(value);
    public static implicit operator string(Base64Guid value) => ToBase64(value.Value);
    public static implicit operator Guid(Base64Guid value) => value.Value;


    public static Base64Guid Parse(string? value) => new(value);

    public static bool TryParse(string? value, out Base64Guid result) {
        try {
            result = Parse(value);
            return true;
        }
        catch {
            result = default;
            return false;
        }
    }

    private static string ToBase64(Guid guid) {
        if (guid == Guid.Empty) return string.Empty;
        return Convert.ToBase64String(guid.ToByteArray())
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '.');
    }

    private static readonly Regex _base64Chars = new(@"^[a-zA-Z0-9\-\.]{22}$", RegexOptions.Compiled);

    private static Guid ToGuid(string? input) {
        if (input is null) return Guid.Empty;
        var text = Ensure.IsNotNullOrWhiteSpace(input).Trim();
        if (!_base64Chars.IsMatch(text))
            throw new FormatException("Invalid base64 url friendly GUID.");

        text = text
              .Replace('.', '/')
              .Replace('-', '+')
             + "==";

        var buffer = Convert.FromBase64String(text);
        return new Guid(buffer);
    }
}