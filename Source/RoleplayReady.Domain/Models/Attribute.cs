namespace RoleplayReady.Domain.Models;

public record Attribute : Entity, IAttribute {
    public Attribute() {

    }

    [SetsRequiredMembers]
    public Attribute(IEntity parent, string ownerId, Type dataType, string abbreviation, string name, string description, State? state = null)
        : base(parent, ownerId, abbreviation, name, description, state) {
        DataType = dataType;
    }

    public required Type DataType { get; init; }
}