namespace RoleplayReady.Domain.Models.Contracts;

public interface IAmOrderedBy {
    int Order { get; init; }
}

public interface IWorkflowStep : IAmOrderedBy, IAmKnownAs, IMayHaveADescription {
    IWorkflow Parent { get; init; }
}