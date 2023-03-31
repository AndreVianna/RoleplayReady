namespace RoleplayReady.Domain.Models;

public record Workflow : Element, IWorkflow {
    public Workflow() {
        
    }

    [SetsRequiredMembers]
    public Workflow(IEntity parent, string ownerId, string name, string? description = null)
        : base(parent, ownerId, name, description) { }

    // RuleSet and Process must be unique.
    public IList<IWorkflowStep> Steps { get; init; } = new List<IWorkflowStep>();
}