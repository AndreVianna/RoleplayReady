namespace RoleplayReady.Domain.Models;

public record Possession : IPossession {
    public Possession() { }

    [SetsRequiredMembers]
    public Possession(IEntity parent, IObject @object, decimal quantity) {
        Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        Object = @object ?? throw new ArgumentNullException(nameof(@object));
        Quantity = quantity;
    }

    public required IEntity Parent { get; init; }
    public required IObject Object { get; init; }
    public required decimal Quantity { get; init; }
}
