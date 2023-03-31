namespace RoleplayReady.Domain.Utilities;

public interface ICheck {
    IConnectBuilders Contains<TValue>(TValue candidate, string message, Severity severity = Suggestion);
    IConnectBuilders ContainsKey<TKey, TValue>(TKey key, string message, Severity severity = Suggestion)
        where TKey : notnull;
    IConnectBuilders IsEqualTo<TValue>(TValue validValue, string message, Severity severity = Suggestion)
        where TValue : IEquatable<TValue>;
    IConnectBuilders IsBetween<TValue>(TValue minimum, TValue maximum, string message, Severity severity = Suggestion)
        where TValue : IComparable<TValue>;
    IConnectBuilders IsGreaterThan<TValue>(TValue minimum, string message, Severity severity = Suggestion)
        where TValue : IComparable<TValue>;
    IConnectBuilders IsGreaterOrEqualTo<TValue>(TValue minimum, string message, Severity severity = Suggestion)
        where TValue : IComparable<TValue>;
    IConnectBuilders IsLessThan<TValue>(TValue maximum, string message, Severity severity = Suggestion)
        where TValue : IComparable<TValue>;
    IConnectBuilders IsLessOrEqualTo<TValue>(TValue maximum, string message, Severity severity = Suggestion)
        where TValue : IComparable<TValue>;
}