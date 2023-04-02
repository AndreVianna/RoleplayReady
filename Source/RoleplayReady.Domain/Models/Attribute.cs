namespace RoleplayReady.Domain.Models;

public record Attribute : IAttribute {
    public Attribute() {

    }

    [SetsRequiredMembers]
    public Attribute(Type dataType, string abbreviation, string name, string description) {
        Abbreviation = abbreviation;
        Name = name;
        Description = description;
        DataType = dataType;
    }

    public required string Abbreviation { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required Type DataType { get; init; }
}