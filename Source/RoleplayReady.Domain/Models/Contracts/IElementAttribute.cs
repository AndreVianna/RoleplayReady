namespace RoleplayReady.Domain.Models.Contracts;

public interface IElementAttribute : IChild, IHaveAttribute, IHaveValue { }

public interface IElementAttribute<TValue> : IElementAttribute, IHaveValue<TValue> { }