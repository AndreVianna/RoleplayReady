namespace RoleplayReady.Domain.Models.Contracts;

public interface IWorkflowStep : IAmKnownAs, IMayHaveADescription {
    IWorkflow Parent { get; init; }
    int Order { get; init; }
}