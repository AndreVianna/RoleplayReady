using System.Runtime.CompilerServices;

namespace RolePlayReady.Utilities;

public static class Throw {
    public static TObject IfNull<TObject>([NotNull] TObject? argument, [CallerArgumentExpression("argument")] string? paramName = null) {
        ArgumentNullException.ThrowIfNull(argument, paramName);
        return argument;
    }

    public static IEnumerable<TItem> IfNullOrEmpty<TItem>([NotNull] IEnumerable<TItem>? argument, [CallerArgumentExpression("argument")] string? paramName = null)
        => argument is ICollection<TItem> collection && collection.Count != 0
            ? argument
            : throw new ArgumentException("Collection cannot be null or empty.", paramName);


    public static string IfNullOrEmpty([NotNull] string? argument, [CallerArgumentExpression("argument")] string? paramName = null)
        => string.IsNullOrEmpty(argument)
            ? throw new ArgumentException("Value cannot be null or empty.", paramName)
            : argument;

    public static string IfNullOrWhiteSpaces([NotNull] string? argument, [CallerArgumentExpression("argument")] string? paramName = null)
        => string.IsNullOrWhiteSpace(argument)
            ? throw new ArgumentException("Value cannot be null or whitespaces.", paramName)
            : argument;
}