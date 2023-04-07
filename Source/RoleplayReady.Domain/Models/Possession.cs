namespace RolePlayReady.Models;

public record Possession : IPossession {
    public Possession() { }

    [SetsRequiredMembers]
    public Possession(IAgent owner, IObject @object, decimal quantity) {
        Owner = Throw.IfNull(owner);
        Object = Throw.IfNull(@object);
        Quantity = quantity;
    }

    public required IAgent Owner { get; init; }
    public required IObject Object { get; init; }
    public required decimal Quantity { get; init; }

    //public IPossession CloneTo(IAgent newOwner) => this with { Owner = newOwner, Object = ((Component)Object).CloneUnder<Object>(newOwner) };
}
