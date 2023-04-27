namespace System.Utilities;

public static class Converter {
    public static string ToBase64Url(this Guid guid) =>
        Convert.ToBase64String(guid.ToByteArray())
            .TrimEnd('=') // Remove padding characters
            .Replace('+', '-') // Replace '+' with '-'
            .Replace('/', '.'); // Replace '/' with '.'

    public static Guid ToGuid(this string input) {
        try {
            var base64Url = Ensure.IsNotNullOrWhiteSpace(input).Trim();
            return base64Url.All(i => i is not '-' or '.')
                ? Guid.Parse(base64Url)
                : ConvertToGuid(base64Url);
        }
        catch {
            throw new FormatException("Unrecognized Guid format.");
        }
    }

    private static Guid ConvertToGuid(string base64Url) {
        base64Url = base64Url.Replace('-', '+').Replace('.', '/');
        var mod4 = base64Url.Length % 4;
        if (mod4 > 0)
            base64Url += new string('=', 4 - mod4);

        var buffer = Convert.FromBase64String(base64Url);
        return new Guid(buffer);
    }
}