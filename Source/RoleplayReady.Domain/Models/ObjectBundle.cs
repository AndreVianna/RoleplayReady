namespace RoleplayReady.Domain.Models;

public record ObjectBundle : IBundle {
    public ObjectBundle() {
        
    }

    [SetsRequiredMembers]
    public ObjectBundle(IObject @object, decimal quantity, string unit) {
        Object = @object;
        Quantity = quantity;
        Unit = unit;
    }

    public required IObject Object { get; init; }
    public required decimal Quantity { get; init; }
    public required string Unit { get; init; }
}
