namespace RoleplayReady.Domain.Models;

public record ProcessStep : IProcessStep {
    public ProcessStep() { }

    [SetsRequiredMembers]
    public ProcessStep(IProcess process, int order, string name, string description) {
        Parent = process ?? throw new ArgumentNullException(nameof(process));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }
    
    // SystemProcess and Order must be unique.
    public required IProcess Parent { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required Func<IEntity, IProcessStep?> Execute { get; init; }
}