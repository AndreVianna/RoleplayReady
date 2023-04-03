using RolePlayReady.Models.Contracts;
using RolePlayReady.Utilities;

namespace RolePlayReady.Models;

public record ProcedureStep : IProcedureStep {
    public ProcedureStep() { }

    [SetsRequiredMembers]
    public ProcedureStep(IProcedure procedure, string abbreviation, string name, string description, Func<IEntity, IProcedureStep?> execute) {
        Procedure = Throw.IfNull(procedure);
        Abbreviation = Throw.IfNullOrWhiteSpaces(abbreviation);
        Name = Throw.IfNullOrWhiteSpaces(name);
        Description = Throw.IfNullOrWhiteSpaces(description);
        Execute = Throw.IfNull(execute);
    }

    // SystemProcess and Order must be unique.
    public required IProcedure Procedure { get; set; }
    public string Type { get; init; }
    public required string Name { get; init; }
    public required string Abbreviation { get; init; }
    public required string Description { get; init; }
    public required Func<IEntity, IProcedureStep?> Execute { get; init; }

    public ISource? Source { get; init; }

    Func<IEntity, ProcedureStepResult?> IProcedureStep.Execute { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }

    public IProcedureStep TransferTo(IProcedure newOwner) => throw new NotImplementedException();
}