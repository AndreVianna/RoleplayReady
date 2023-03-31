namespace RoleplayReady.Domain.Models.Contracts;

public interface IHaveSteps {
    public IList<IWorkflowStep> Steps { get; init; }
}