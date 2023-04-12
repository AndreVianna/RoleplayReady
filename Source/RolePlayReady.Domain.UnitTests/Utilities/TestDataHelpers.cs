namespace RolePlayReady.Utilities;

public static class TestDataHelpers {
    internal static string? GenerateTestString(int? size) =>
        size switch {
            null => default,
            0 => string.Empty,
            < 0 => new(' ', -size.Value), // negative size means whitespace
            _ => new('X', size.Value), // positive size means Xs
        };

    internal static TItem?[]? GenerateTestCollection<TItem>(int? size, TItem? value = default) =>
        size switch {
            null => default,
            0 => Array.Empty<TItem>(),
            < 0 => Enumerable.Range(0, -(int)size).Select(_ => default(TItem)).ToArray(), // negative size means nulls
            _ => Enumerable.Range(0, (int)size).Select(_ => value).ToArray(),
        };
}