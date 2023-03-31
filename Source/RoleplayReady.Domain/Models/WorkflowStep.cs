namespace RoleplayReady.Domain.Models;

public record WorkflowStep : IWorkflowStep {
    public WorkflowStep() {
        
    }

    [SetsRequiredMembers]
    public WorkflowStep(IWorkflow workflow, int order, string name, string? description = null) {
        Parent = workflow;
        Order = order;
        Name = name;
        Description = description;
    }
    
    // SystemProcess and Order must be unique.
    public required IWorkflow Parent { get; init; }
    public required int Order { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
}