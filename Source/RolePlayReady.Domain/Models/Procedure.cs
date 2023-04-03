using RolePlayReady.Models.Contracts;
using RolePlayReady.Utilities.Contracts;

namespace RolePlayReady.Models;

public record Procedure : Component, IProcedure {
    public Procedure() { }

    [SetsRequiredMembers]
    public Procedure(IComponent parent, string abbreviation, string name, string description, IProcedureStep start, IDateTimeProvider? dateTimeProvider)
        : base(parent, abbreviation, name, description, dateTimeProvider) {
        Start = start;
    }

    public required IProcedureStep Start { get; init; }
}