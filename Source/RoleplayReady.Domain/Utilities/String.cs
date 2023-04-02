﻿using System.Runtime.CompilerServices;

namespace RoleplayReady.Domain.Utilities;

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

public static class String {

    private static readonly Regex _splitIntoWordsRegex = new Regex(@"[^a-zA-Z0-9]+", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

    public static string ToPascalCase(this string input) {
        var words = _splitIntoWordsRegex.Split(input.Trim().Replace("'", "")).Where(s => s != string.Empty).ToArray();

        if (words.Length == 0)
            return string.Empty;
        var result = new StringBuilder();
        foreach (var word in words)
            result.Append(char.ToUpper(word[0])).Append(word[1..].ToLower());
        return result.ToString();
    }

    public static string ToCamelCase(this string input) {
        var words = _splitIntoWordsRegex.Split(input.Trim().Replace("'", "")).Where(s => s != string.Empty).ToArray();


        if (words.Length == 0)
            return string.Empty;
        var result = new StringBuilder();
        result.Append(char.ToLower(words[0][0])).Append(words[0][1..].ToLower());
        if (words.Length == 1)
            return result.ToString();
        foreach (var word in words[1..])
            result.Append(char.ToUpper(word[0])).Append(word[1..].ToLower());
        return result.ToString();
    }

    private static readonly HashSet<string> _specialWords = new() {
        "a", "an", "and", "as", "at", "by", "for", "from", "in", "of", "on", "or", "the", "to", "with"
    };

    public static string ToAcronym(this string input) {
        var words = _splitIntoWordsRegex.Split(input.Trim().Replace("'", "")).Where(s => s != string.Empty).ToArray();

        if (words.Length == 0)
            return string.Empty;
        var result = new StringBuilder();
        result.Append(char.ToUpper(words[0][0]));
        if (words.Length == 1)
            return result.ToString();
        foreach (var s in words[1..])
            result.Append(GetFirstLetter(s));
        return result.ToString();

        static char GetFirstLetter(string word) => _specialWords.Contains(word) ? char.ToLower(word[0]) : char.ToUpper(word[0]);
    }
}