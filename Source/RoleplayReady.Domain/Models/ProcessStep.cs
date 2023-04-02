namespace RoleplayReady.Domain.Models;

public record ProcessStep : IProcessStep {
    public ProcessStep() { }

    [SetsRequiredMembers]
    public ProcessStep(IProcess process, string abbreviation, string name, string description, Func<IEntity, IProcessStep?> execute) {
        Parent = Throw.IfNull(process);
        Abbreviation = Throw.IfNullOrWhiteSpaces(abbreviation);
        Name = Throw.IfNullOrWhiteSpaces(name);
        Description = Throw.IfNullOrWhiteSpaces(description);
        Execute = Throw.IfNull(execute);
    }

    // SystemProcess and Order must be unique.
    public required IProcess Parent { get; init; }
    public required string Abbreviation { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required Func<IEntity, IProcessStep?> Execute { get; init; }
}