namespace RolePlayReady.Models;

public record Object : Entity, IObject {
    private readonly string _unit = string.Empty;

    public required string Unit {
        get => _unit;
        init => _unit = Throw.IfNullOrWhiteSpaces(value);
    }
}