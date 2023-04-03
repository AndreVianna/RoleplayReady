using RolePlayReady.Models.Contracts;
using RolePlayReady.Utilities;

namespace RolePlayReady.Models;

public record Possession : IPossession {
    public Possession() { }

    [SetsRequiredMembers]
    public Possession(IActor owner, IObject @object, decimal quantity) {
        Owner = Throw.IfNull(owner);
        Object = Throw.IfNull(@object);
        Quantity = quantity;
    }

    public required IActor Owner { get; init; }
    public required IObject Object { get; init; }
    public required decimal Quantity { get; init; }

    public IPossession CloneTo(IActor newOwner) => this with { Owner = newOwner, Object = ((Component)Object).CloneUnder<Object>(newOwner) };
}
