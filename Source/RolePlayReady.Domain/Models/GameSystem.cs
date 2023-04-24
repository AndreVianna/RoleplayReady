namespace RolePlayReady.Models;

public record GameSystem : Persisted {
    public ICollection<Base> Domains { get; init; } = new List<Base>();
}