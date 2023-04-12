using static System.Constants.Constants.ErrorMessages;

namespace System;

public static class Ensure {
    [return: NotNull]
    public static TObject NotNull<TObject>(TObject? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        => argument is null
            ? throw new ArgumentNullException(paramName, string.Format(Null, paramName))
            : argument;


    [return: NotNull]
    public static string NotNullOrEmpty(string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        => NotNull(argument, paramName).Length == 0
            ? throw new ArgumentException(string.Format(Empty, paramName), paramName)
            : argument!;

    [return: NotNull]
    public static string NotNullOrWhiteSpace(string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        => NotNullOrEmpty(argument, paramName).Trim().Length == 0
            ? throw new ArgumentException(string.Format(Whitespace, paramName), paramName)
            : argument!;

    [return: NotNull]
    public static ICollection<TItem> NotNullOrHasNull<TItem>(ICollection<TItem>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        => NotNull(argument, paramName).Any(x => x is null)
            ? throw new ArgumentNullException(paramName, string.Format(HasNull, paramName))
            : argument!;

    [return: NotNull]
    public static ICollection<string> NotNullOrHasNullOrEmpty(ICollection<string>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        => NotNull(argument, paramName).Any(string.IsNullOrEmpty)
            ? throw new ArgumentNullException(paramName, string.Format(HasNullOrEmpty, paramName))
            : argument!;

    [return: NotNull]
    public static ICollection<string> NotNullOrHasNullOrWhiteSpace(ICollection<string>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        => NotNull(argument, paramName).Any(string.IsNullOrWhiteSpace)
            ? throw new ArgumentNullException(paramName, string.Format(HasNullOrWhitespace, paramName))
            : argument!;

    [return: NotNull]
    public static ICollection<TItem> NotNullOrEmpty<TItem>(ICollection<TItem>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        => !NotNull(argument, paramName).Any()
            ? throw new ArgumentException(string.Format(Empty, paramName), paramName)
            : argument!;

    [return: NotNull]
    public static ICollection<TItem> NotNullOrEmptyOrHasNull<TItem>(ICollection<TItem>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        => NotNullOrEmpty(argument, paramName).Any(x => x is null)
            ? throw new ArgumentException(string.Format(HasNull, paramName), paramName)
            : argument!;

    [return: NotNull]
    public static ICollection<string> NotNullOrEmptyOrHasNullOrEmpty(ICollection<string>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        => NotNullOrEmpty(argument, paramName).Any(string.IsNullOrEmpty)
            ? throw new ArgumentException(string.Format(HasNullOrEmpty, paramName), paramName)
            : argument!;

    [return: NotNull]
    public static ICollection<string> NotNullOrEmptyOrHasNullOrWhiteSpace(ICollection<string>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        => NotNullOrEmpty(argument, paramName).Any(string.IsNullOrWhiteSpace)
            ? throw new ArgumentException(string.Format(HasNullOrWhitespace, paramName), paramName)
            : argument!;
}