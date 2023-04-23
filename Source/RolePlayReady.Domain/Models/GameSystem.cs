namespace RolePlayReady.Models;

public record GameSystem : Base {
    public ICollection<Domain> Domains { get; init; } = new List<Domain>();

    public ICollection<Base> ComponentDefinitions { get; init; } = new List<Base>();
}