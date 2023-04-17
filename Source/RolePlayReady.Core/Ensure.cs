namespace System;

public static class Ensure {
    [return: NotNull]
    public static TArgument IsNotNull<TArgument>(TArgument? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        => argument is null
            ? throw new ArgumentNullException(paramName, string.Format(CannotBeNull, paramName))
            : argument;

    [return: NotNull]
    public static TArgument IsOfType<TArgument>(object? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        => IsNotNull(argument, paramName) is not TArgument result
            ? throw new ArgumentException(string.Format(IsNotOfType, paramName, typeof(TArgument).Name, argument!.GetType().Name), paramName)
            : result;

    public static string IsNotNullOrEmpty(string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        => IsNotNull(argument, paramName).Length == 0
            ? throw new ArgumentException(string.Format(CannotBeEmpty, paramName), paramName)
            : argument!;

    public static string IsNotNullOrWhiteSpace(string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        => IsNotNullOrEmpty(argument, paramName).Trim().Length == 0
            ? throw new ArgumentException(string.Format(CannotBeWhitespace, paramName), paramName)
            : argument!;

    public static ICollection<TItem> IsNotNullAndHasNoNullItems<TItem>(IEnumerable<TItem>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null) {
        var collection = IsOfType<ICollection<TItem>>(argument, paramName);
        return collection.Any(x => x is null)
            ? throw new ArgumentNullException(paramName, string.Format(CannotContainNull, paramName))
            : collection;
    }

    [SuppressMessage("Style", "IDE0200:Remove unnecessary lambda expression", Justification = "Impacts code coverage.")]
    public static ICollection<string> IsNotNullAndHasNoNullOrEmptyItems(IEnumerable<string>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        var collection = IsOfType<ICollection<string>>(argument, paramName);
        // ReSharper disable once ConvertClosureToMethodGroup - Impacts code coverage,
        return collection.Any(i => string.IsNullOrEmpty(i))
            ? throw new ArgumentNullException(paramName, string.Format(CannotContainNullOrEmpty, paramName))
            : collection!;
    }

    [SuppressMessage("Style", "IDE0200:Remove unnecessary lambda expression", Justification = "Impacts code coverage.")]
    public static ICollection<string> IsNotNullAndHasNoNullOrWhiteSpaceItems(IEnumerable<string>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        var collection = IsOfType<ICollection<string>>(argument, paramName);
        // ReSharper disable once ConvertClosureToMethodGroup - Impacts code coverage,
        return collection.Any(i => string.IsNullOrWhiteSpace(i))
            ? throw new ArgumentNullException(paramName, string.Format(CannotContainNullOrWhitespace, paramName))
            : collection!;
    }

    public static ICollection<TItem> IsNotNullOrEmpty<TItem>(IEnumerable<TItem>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null) {
        var collection = IsOfType<ICollection<TItem>>(argument, paramName);
        return !collection.Any()
            ? throw new ArgumentException(string.Format(CannotBeEmpty, paramName), paramName)
            : collection;
    }

    public static ICollection<TItem> IsNotNullOrEmptyAndHasNoNullItems<TItem>(IEnumerable<TItem>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null) {
        var collection = IsNotNullOrEmpty(argument, paramName);
        return collection.Any(x => x is null)
            ? throw new ArgumentException(string.Format(CannotContainNull, paramName), paramName)
            : collection!;
    }

    [SuppressMessage("Style", "IDE0200:Remove unnecessary lambda expression", Justification = "Impacts code coverage.")]
    public static ICollection<string> IsNotNullOrEmptyAndHasNoNullOrEmptyItems(IEnumerable<string>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        var collection = IsNotNullOrEmpty(argument, paramName);
        // ReSharper disable once ConvertClosureToMethodGroup - Impacts code coverage,
        return collection.Any(i => string.IsNullOrEmpty(i))
            ? throw new ArgumentException(string.Format(CannotContainNullOrEmpty, paramName), paramName)
            : collection;
    }

    [SuppressMessage("Style", "IDE0200:Remove unnecessary lambda expression", Justification = "Impacts code coverage.")]
    public static ICollection<string> IsNotNullOrEmptyAndHasNoNullOrWhiteSpaceItems(IEnumerable<string>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        var collection = IsNotNullOrEmpty(argument, paramName);
        // ReSharper disable once ConvertClosureToMethodGroup - Impacts code coverage,
        return IsNotNullOrEmpty(collection, paramName).Any(i => string.IsNullOrWhiteSpace(i))
            ? throw new ArgumentException(string.Format(CannotContainNullOrWhitespace, paramName), paramName)
            : collection;
        }
}