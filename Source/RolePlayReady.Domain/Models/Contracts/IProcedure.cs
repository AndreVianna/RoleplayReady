namespace RolePlayReady.Models.Contracts;

public interface IProcedure : ITransferable<IProcedureStep, IProcedure> {
    IEntity RuleSet { get; init; }
    IProcedureStep Start { get; init; }
    string Type { get; init; }
    string Abbreviation { get; init; }
    string Name { get; init; }
    string Description { get; init; }
}