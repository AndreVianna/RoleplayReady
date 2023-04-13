namespace System;

public static class Ensure {
    [return: NotNull]
    public static TArgument NotNull<TArgument>(TArgument? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        => argument is null
            ? throw new ArgumentNullException(paramName, string.Format(IsRequired, paramName))
            : argument;

    [return: NotNull]
    public static TArgument OfType<TArgument>(object? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        => NotNull(argument, paramName) is not TArgument result
            ? throw new ArgumentException(string.Format(IsNotOfType, paramName, typeof(TArgument).Name), paramName)
            : result;

    public static string NotNullOrEmpty(string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        => NotNull(argument, paramName).Length == 0
            ? throw new ArgumentException(string.Format(CannotBeEmpty, paramName), paramName)
            : argument!;

    public static string NotNullOrWhiteSpace(string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        => NotNullOrEmpty(argument, paramName).Trim().Length == 0
            ? throw new ArgumentException(string.Format(CannotBeWhitespace, paramName), paramName)
            : argument!;

    public static ICollection<TItem> NotNullOrHasNull<TItem>(IEnumerable<TItem>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null) {
        var collection = OfType<ICollection<TItem>>(argument, paramName);
        return collection.Any(x => x is null)
            ? throw new ArgumentNullException(paramName, string.Format(CannotContainNull, paramName))
            : collection;
    }

    [SuppressMessage("Style", "IDE0200:Remove unnecessary lambda expression", Justification = "Impacts code coverage.")]
    public static ICollection<string> NotNullOrHasNullOrEmpty(IEnumerable<string>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        var collection = OfType<ICollection<string>>(argument, paramName);
        // ReSharper disable once ConvertClosureToMethodGroup - Impacts code coverage,
        return collection.Any(i => string.IsNullOrEmpty(i))
            ? throw new ArgumentNullException(paramName, string.Format(CannotContainNullOrEmpty, paramName))
            : collection!;
    }

    [SuppressMessage("Style", "IDE0200:Remove unnecessary lambda expression", Justification = "Impacts code coverage.")]
    public static ICollection<string> NotNullOrHasNullOrWhiteSpace(IEnumerable<string>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        var collection = OfType<ICollection<string>>(argument, paramName);
        // ReSharper disable once ConvertClosureToMethodGroup - Impacts code coverage,
        return collection.Any(i => string.IsNullOrWhiteSpace(i))
            ? throw new ArgumentNullException(paramName, string.Format(CannotContainNullOrWhitespace, paramName))
            : collection!;
    }

    public static ICollection<TItem> NotNullOrEmpty<TItem>(IEnumerable<TItem>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null) {
        var collection = OfType<ICollection<TItem>>(argument, paramName);
        return !collection.Any()
            ? throw new ArgumentException(string.Format(CannotBeEmpty, paramName), paramName)
            : collection;
    }

    public static ICollection<TItem> NotNullOrEmptyOrHasNull<TItem>(IEnumerable<TItem>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null) {
        var collection = NotNullOrEmpty(argument, paramName);
        return collection.Any(x => x is null)
            ? throw new ArgumentException(string.Format(CannotContainNull, paramName), paramName)
            : collection!;
    }

    [SuppressMessage("Style", "IDE0200:Remove unnecessary lambda expression", Justification = "Impacts code coverage.")]
    public static ICollection<string> NotNullOrEmptyOrHasNullOrEmpty(IEnumerable<string>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        var collection = NotNullOrEmpty(argument, paramName);
        // ReSharper disable once ConvertClosureToMethodGroup - Impacts code coverage,
        return collection.Any(i => string.IsNullOrEmpty(i))
            ? throw new ArgumentException(string.Format(CannotContainNullOrEmpty, paramName), paramName)
            : collection;
    }

    [SuppressMessage("Style", "IDE0200:Remove unnecessary lambda expression", Justification = "Impacts code coverage.")]
    public static ICollection<string> NotNullOrEmptyOrHasNullOrWhiteSpace(IEnumerable<string>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        var collection = NotNullOrEmpty(argument, paramName);
        // ReSharper disable once ConvertClosureToMethodGroup - Impacts code coverage,
        return NotNullOrEmpty(collection, paramName).Any(i => string.IsNullOrWhiteSpace(i))
            ? throw new ArgumentException(string.Format(CannotContainNullOrWhitespace, paramName), paramName)
            : collection;
        }
}