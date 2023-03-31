namespace RoleplayReady.Domain.Models.Contracts;

public interface IHaveValue
{
    object? Value { get; set; }
}

public interface IHaveValue<TValue> : IHaveValue
{
    new TValue? Value { get; set; }
}

