namespace RoleplayReady.Domain.Models;

public record Attribute : Child, IAttribute {
    public Attribute() {

    }

    [SetsRequiredMembers]
    public Attribute(IEntity parent, string ownerId, Type dataType, string name, string? description = null, Status? status = null)
        : base(parent, ownerId, name, description, status) {
        DataType = dataType;
    }

    public required Type DataType { get; init; }
}