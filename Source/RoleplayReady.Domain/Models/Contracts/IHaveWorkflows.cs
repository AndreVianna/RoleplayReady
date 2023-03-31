namespace RoleplayReady.Domain.Models.Contracts;

public interface IHaveWorkflows {
    public IList<IWorkflow> Workflows { get; init; }
}