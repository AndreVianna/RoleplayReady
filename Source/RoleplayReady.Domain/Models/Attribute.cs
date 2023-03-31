namespace RoleplayReady.Domain.Models;

public record Attribute : Child, IAttribute {
    public Attribute() {

    }

    [SetsRequiredMembers]
    public Attribute(IEntity parent, string ownerId, string name, Type dataType, string? description = null)
        : base(parent, ownerId, name, description) {
        DataType = dataType;
    }

    public required Type DataType { get; init; }
}