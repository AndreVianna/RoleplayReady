namespace RolePlayReady;

public static class Throw {
    private const string _emptyCollectionMessage = "The collection cannot be null or empty.";
    private const string _nullItemsMessage = "The collection cannot contain null items.";
    private const string _nullValueMessage = "The value cannot be null.";
    private const string _nullOrEmptyMessage = "The value cannot be null or empty.";
    private const string _nullOrWhitespacesMessage = "The value cannot be null or whitespaces.";

    public static TObject IfNull<TObject>([NotNull] TObject? argument, string? message = null, [CallerArgumentExpression("argument")] string? paramName = null)
        => argument is null
            ? throw new ArgumentNullException(paramName, GetMessageOrDefault(message, _nullValueMessage))
            : argument;

    public static IEnumerable<TItem> IfNullOrEmpty<TItem>([NotNull] IEnumerable<TItem>? argument, string? message = null, [CallerArgumentExpression("argument")] string? paramName = null)
        => argument is not ICollection<TItem> collection || collection.Count == 0
            ? ThrowArgumentException<IEnumerable<TItem>>(message, _emptyCollectionMessage, paramName)
            : argument;

    public static IEnumerable<TItem> IfNullOrEmptyOrContainNulls<TItem>([NotNull] IEnumerable<TItem?>? argument, string? message = null, [CallerArgumentExpression("argument")] string? paramName = null)
        => argument is not ICollection<TItem> collection || collection.Count == 0
            ? ThrowArgumentException<IEnumerable<TItem>>(message, _emptyCollectionMessage, paramName)
            : !collection.All(x => x is not null)
                ? ThrowArgumentException<IEnumerable<TItem>>(message, _nullItemsMessage, paramName)
                : collection;

    public static string IfNullOrEmpty([NotNull] string? argument, string? message = null, [CallerArgumentExpression("argument")] string? paramName = null)
        => string.IsNullOrEmpty(argument)
            ? ThrowArgumentException<string>(message, _nullOrEmptyMessage, paramName)
            : argument;

    public static string IfNullOrWhiteSpaces([NotNull] string? argument, string? message = null, [CallerArgumentExpression("argument")] string? paramName = null)
        => string.IsNullOrWhiteSpace(argument)
            ? ThrowArgumentException<string>(message, _nullOrWhitespacesMessage, paramName)
            : argument;

    private static string GetMessageOrDefault(string? message, string defaultMessage)
        => !string.IsNullOrWhiteSpace(message) ? message : defaultMessage;

    [DoesNotReturn]
    private static TReturn ThrowArgumentException<TReturn>(string? message, string defaultMessage, string? paramName)
        => throw new ArgumentException(GetMessageOrDefault(message, defaultMessage), paramName);
}