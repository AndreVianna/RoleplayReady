namespace RolePlayReady.Handlers.System;

public record System : Persisted {
    public ICollection<Base> Domains { get; init; } = [];
}
