namespace RoleplayReady.Domain.Models;

public record Workflow : Element, IWorkflow {
    public Workflow() {

    }

    [SetsRequiredMembers]
    public Workflow(IEntity parent, string ownerId, string name, string? description = null, Status? status = null, Usage? usage = null, ISource? source = null)
        : base(parent, ownerId, name, description, status, usage, source) { }

    // RuleSet and Process must be unique.
    public IList<IWorkflowStep> Steps { get; init; } = new List<IWorkflowStep>();
}