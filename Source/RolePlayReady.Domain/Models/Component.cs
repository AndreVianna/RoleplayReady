namespace RolePlayReady.Models;

public abstract record Component : Persisted {
    public ICollection<IAttribute> Attributes { get; init; } = [];
}