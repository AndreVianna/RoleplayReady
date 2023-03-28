namespace RoleplayReady.Domain.Models;

public interface IHasValue
{
    object? Value { get; set; }
}

public interface IHasValue<TValue> : IHasValue
{
    new TValue? Value { get; set; }
}

