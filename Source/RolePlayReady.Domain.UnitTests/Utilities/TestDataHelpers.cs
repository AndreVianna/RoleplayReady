using System.Extensions;

namespace RolePlayReady.Utilities;

public static class TestDataHelpers {
    internal static string? GenerateTestString(int? size)
        => size switch {
            null => default,
            0 => string.Empty,
            < 0 => new(' ', -size.Value), // negative size means whitespace
            _ => new('X', size.Value), // positive size means Xs
        };

    internal static ICollection<TItem>? GenerateTestCollection<TItem>(int? count, TItem value)
        => count switch {
            null => default,
            0 => Array.Empty<TItem>(),
            < 0 => Enumerable.Range(0, -(int)count).ToArray(_ => default(TItem)!), // negative size means use defaults
            _ => Enumerable.Range(0, (int)count).ToArray(_ => value), // positive size means use value
        };

    internal static IDictionary<TKey, TValue>? GenerateTestDictionary<TKey, TValue>(int? count, TValue? value)
        where TKey : notnull => count switch {
            null => default,
            0 => new Dictionary<TKey, TValue>(),
            < 0 when typeof(TKey) == typeof(string) => GenerateTestDictionaryWithStringKey<TKey, TValue>(-count.Value, default), // negative size means use default
            < 0 when typeof(TKey) == typeof(int) => GenerateTestDictionaryWithIntKey<TKey, TValue>(-count.Value, default), // negative size means use default
            _ when typeof(TKey) == typeof(string) => GenerateTestDictionaryWithStringKey<TKey, TValue>(count.Value, value), // positive size means use value
            _ => GenerateTestDictionaryWithIntKey<TKey, TValue>(count.Value, value), // positive size means use value
        };

    private static IDictionary<TKey, TValue> GenerateTestDictionaryWithStringKey<TKey, TValue>(int count, TValue? value)
        => (IDictionary<TKey, TValue>)Enumerable.Range(0, count).ToDictionary(i => $"{i}", _ => value);

    private static IDictionary<TKey, TValue> GenerateTestDictionaryWithIntKey<TKey, TValue>(int count, TValue? value)
        => (IDictionary<TKey, TValue>)Enumerable.Range(0, count).ToDictionary(i => i, _ => value);
}